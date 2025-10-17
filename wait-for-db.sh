#!/bin/bash
# wait-for-db.sh

# Definir o n�mero de tentativas e o tempo entre tentativas
RETRIES=10
WAIT=5

# Esperar at� que o banco de dados esteja acess�vel
for i in $(seq 1 $RETRIES); do
    if psql -h esg-db -U esguser -d esg_cidades -c '\q' 2>/dev/null; then
        echo "Banco de dados est� pronto!"
        break
    else
        echo "Tentando conectar ao banco de dados... Tentativa $i de $RETRIES"
        sleep $WAIT
    fi
done

# Se n�o conseguir se conectar ap�s as tentativas, falha
if [ $i -eq $RETRIES ]; then
    echo "N�o foi poss�vel conectar ao banco de dados ap�s $RETRIES tentativas."
    exit 1
fi
