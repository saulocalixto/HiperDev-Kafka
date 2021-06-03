# Calixto's Store
Sistema simples para exemplificar como podemos usar o _Apache Kafka_ para integração de microserviços.
Para tanto foi implementado um sistema principal que manipula clientes e salva seus dados em um banco SqlServer. Esse sistema também envia cada alteração do cliente para tópicos do Kafka que serão consumidos por: _Ms-HistoricoClientes_ e _CalixtosStore.Email_.

##  Ms-HistoricoClientes
Funciona como um sourcing de alterações feitas em cliente. Cada alteração no cliente é enviado para um tópico que esse microserviço lê, processa a mensagem e salva em um banco de dados mongo. Posteriormente esse histórico pode disponibilizar esse histórico para o sistema principal.

## CalixtosStore.Email
Consumidor simples que lê o tópico de registros de clientes, enviando um e-mail de boas vindas para cada cliente novo cadastrado.

## Pré-requisitos
É preciso ter o docker desktop para windows instalado https://docs.docker.com/docker-for-windows/install/.
É preciso ter uma instância localDb do SQL Server rodando.

### Apache Kafka
Para instalar o Apache Kafka basta entrar na pasta onde é guardado o arquivo de configuração do container

`cd .\DockerCompose\Kafka`

Posteriormente rodar o comando:

`docker-compose up -d` 

Pronto, o Kafka já está rodando.

### MongoDb
O MongoDb é usado no projeto _Ms-HistoricoClientes_ e guarda todo histórico de alteração de um cliente, desde seu registro até sua exclusão.
Para instalar o MongoDb em seu ambiente basta rodar o comando abaixo:

`docker run --name nginx -p 27017:27017 mongo`

Ele irá rodar um container com o mongodb na porta _27017_ do host, a mesma configurada no _Ms-HistoricoClientes_, caso precise rodar em outra porta basta trocar a porta do _appsettings.json_ do projeto.

## Informações gerais
Procure rodar o Producer e os Consumers no ISS, pois se tentar rodar no docker não vai ter como fazer a comunicação entre os containers que estarão rodando o Kafka e o MongoDb.
O projeto foi criado para exemplificar como poderíamos usar o Kafka para integração de microserviços, portanto pode haver algumas má práticas de desenvolvimento.
