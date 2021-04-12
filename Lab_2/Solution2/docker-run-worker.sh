
PORT=$2
NAME=$1
docker run \
    -p $PORT:6000 \
    --name $NAME \
    -v ~/Education/mephi-parprog-2020/material/Docker/data:/data \
    -e "WORKER_PUBLIC_HOSTNAME=${NAME}" \
    -e "WORKER_PUBLIC_PORT=${PORT}" \
    --network distributed_system_network \
    registry.gitlab.com/alextitova/mephi-parprog-2020/worker $1 $2 $3
