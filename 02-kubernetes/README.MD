

1. Установить контроллер nginx
helm upgrade --install ingress-nginx ingress-nginx --repo https://kubernetes.github.io/ingress-nginx  --namespace ingress-nginx --create-namespace

2. Установить mongodb
helm repo add mongodb https://mongodb.github.io/helm-charts
helm install community-operator mongodb/community-operator --namespace otus-mongo --create-namespace
helm install otus-mongo 02-kubernetes/db/.helm --namespace otus-mongo --create-namespace --set "global.password=mypassword"

3. Установить приложение
helm install hw-02-kubernetes 02-kubernetes/backend/.helm --namespace otus --create-namespace --set "global.password=mypassword"

4. Запустить newman
newman run 02-kubernetes/hw-02-kubernetes.postman_collection.json