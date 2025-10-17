#!/bin/bash
# wait-for-db.sh

# Definir o número de tentativas e o tempo entre tentativas
RETRIES=10
WAIT=5

# Esperar até que o banco de dados esteja acessível
for i in $(seq 1 $RETRIES); do
    if psql -h esg-db -U esguser -d esg_cidades -c '\q' 2>/dev/null; then
        echo "Banco de dados está pronto!"
        break
    else
        echo "Tentando conectar ao banco de dados... Tentativa $i de $RETRIES"
        sleep $WAIT
    fi
done

# Se não conseguir se conectar após as tentativas, falha
if [ $i -eq $RETRIES ]; then
    echo "Não foi possível conectar ao banco de dados após $RETRIES tentativas."
    exit 1
fi
