# Tecnologias usadas
- .NET Framework 4.8
- Redis
- Ninject
- NHibernate
- SQL Server

# Tutorial
1.	Descompacte o arquivo ClienteCrud
2.	No SQL Server localhost, rode o script do arquivo Script.sql
3.	Instale o Redis no localhost porta 6379:
a.	Baixe o Redis para Windows em:
https://github.com/microsoftarchive/redis/releases
(Recomendo a versão redis-3.0.504.zip)
4.	Abra o projeto ClienteCrud.sln da pasta ClienteCrud
5.	Reinstale os pacotes Nuget pela linha comando
a.	Ferramentas > Gerenciador de Pacotes do NuGet > Console do Gerenciador de Pacotes
b.	Digite “Update-Package –reinstall” e aperte Enter
c.	Caso aparareça a mensagem “O arquivo 'Scripts\jquery-3.7.1.slim.js' já existe no projeto 'ClienteCrud'. Deseja substituí-lo?”, digite T e aperte Enter
6.	Execute com F5
