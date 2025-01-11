docker-compose down --remove-orphans
docker builder prune -a --force
docker system prune -a -f