docker run \
    -p 5000:5000 \
    --name manager \
    -v ~/lab2/task/lab2/data:/data \
    -e "MANAGER_PUBLIC_HOSTNAME=manager" \
    -e "MANAGER_PUBLIC_PORT=5000" \
    --network distributed_system_network \
    registry.gitlab.com/alextitova/mephi-parprog-2020/manager
