PodCastPipocaAgilApi  🚀


# Configurando o Ambiente de Desenvolvimento
Seja bem-vindo ao projeto! Siga as etapas abaixo para configurar seu ambiente de desenvolvimento e começar a contribuir.

### Requisitos Prévios
.NET Core SDK:<br>
Certifique-se de ter o .NET Core SDK instalado em seu sistema.<br>
Configuração do Entity Framework Core<br>
Instale a Ferramenta dotnet-ef:


### Crie um projeto digitando o seguinte comando:
```sh
    dotnet new webapi --name  PodCastPipocaAgilApi
```

### Execute os seguintes comandos para instalar e atualizar a ferramenta dotnet-ef:
```sh
    dotnet tool install --global dotnet-ef
    dotnet tool update --global dotnet-ef
```

### Execute os seguintes comandos para adicionar os pacotes necessários do Entity Framework Core:
```sh
    dotnet add package Microsoft.EntityFrameworkCore
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.Tools
    dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 7.0.0
```

### Execute o seguinte comando para instalar o pacote do SQL Server e ADO.NET:
```sh
    dotnet add package Microsoft.Data.SqlClient --version 5.2.0-preview3.23201.1
```


### Gerenciamento de Migrações com Entity Framework Core

### [Migrations](https://learn.microsoft.com/pt-br/ef/core/cli/dotnet#dotnet-ef-migrations-list)


```sh
    dotnet ef migrations add Start --context DataContext
    dotnet ef database update --context DataContext
    dotnet ef database instead. --context DataContext

    dotnet ef migrations remove  --context DataContext

    dotnet ef dbcontext info --context DataContext
    dotnet ef dbcontext list --context DataContext
    dotnet ef dbcontext script

    select @@version;
    8.0.23

    DROP TABLE "__EFMigraçõesHistórico"
    delete from __EFMigraçõesHistórico;
```

### Documentação da API

### Configuração do Swagger

A configuração do Swagger foi realizada da seguinte forma:

- **Título:** PodquestPipocaWebApi
- **Versão:** v1
- **Descrição:** Funcionalidade 'Trilha de Conhecimento' no Podcast Pipoca Ágil. Explore vídeos sobre Metodologias Ágeis, Inovação, Gerenciamento de Projetos, e mais. Acesse trilhas personalizadas, e receba recomendações gratuitamente.
- **Termos de Serviço:** [Termos de Serviço](https://meusite.com)
- **Contato:** Victor Sérgio, [meusite.com](https://meusite.com)
- **Licença:** Podquest Pipoca Agil, [Detalhes da Licença](https://meusite.com)





### Configuração de Envio de E-mail com SMTP do Gmail

Este projeto utiliza o serviço SMTP do Gmail para enviar e-mails. Siga os passos abaixo para configurar o envio de e-mails:

1. **Assista ao Tutorial:** [Configuração de Envio de E-mail com SMTP do Gmail](https://www.youtube.com/watch?v=TrdWr3BmqT8)

2. **Teste o Envio de E-mails:**
   - Execute o aplicativo e verifique se o envio de e-mails está funcionando corretamente.
   - Utilize ferramentas online como [SMTP Tester](https://www.gmass.co/smtp-test) para validar as configurações do SMTP.

3. **Configuração no Código:**
   - Abra o arquivo de configuração do aplicativo (Pasta `EmailService`).
   - Adicione as informações de configuração para o envio de e-mails, incluindo o servidor SMTP, porta, nome de usuário e senha.
     
     ```json
     {
       "SmtpSettings": {
         "Server": "smtp.gmail.com",
         "Port": 587,
         "Username": "seu-email@gmail.com",
         "Password": "sua-senha-de-app-gerada"
       }
     }
     ```
   
   Certifique-se de substituir `"seu-email@gmail.com"` e `"sua-senha-de-app-gerada"` pelas suas informações.

4. **Teste de SMTP:**
   - Após configurar, faça um teste de SMTP utilizando [SMTP Tester](https://www.gmass.co/smtp-test) para garantir que as configurações estejam corretas.

Certifique-se de seguir esses passos cuidadosamente para garantir uma configuração bem-sucedida do envio de e-mails. Se você encontrar problemas, revise as etapas acima e assegure-se de que cada passo tenha sido seguido corretamente.
