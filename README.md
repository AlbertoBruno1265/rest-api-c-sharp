# WebService FIAP - API ESG

API REST em .NET 8 para gerenciamento de coleta e reciclagem.

## Requisitos

- .NET 8 SDK
- Oracle FIAP
- Postman ou Insomnia

## Configuracao

Edite `WebServiceFiap/WebServiceFiap/appsettings.Development.json`:

```json
"User ID=SEU_USUARIO;Password=SUA_SENHA;"
```

Depois aplique a migration:

```powershell
dotnet ef database update --project WebServiceFiap\WebServiceFiap\WebServiceFiap.csproj
```

## Rodar API

```powershell
dotnet run --project WebServiceFiap\WebServiceFiap\WebServiceFiap.csproj
```

URL padrao:

```text
http://localhost:5108
```

## Autenticacao

Crie um usuario em `POST /Usuario` e faca login em:

```http
POST /Auth/login
```

Use o token retornado como Bearer Token para acessar endpoints protegidos.

## Testes

```powershell
dotnet test WebServiceFiap\WebServiceFiap.slnx
```

## Endpoints principais

- `GET /Itens?page=1&pageSize=10`
- `GET /CentrosColeta?page=1&pageSize=10`
- `GET /Catadores?page=1&pageSize=10`
- `GET /Descartadores?page=1&pageSize=10`
- `GET /Coletas?page=1&pageSize=10`
- `POST /Auth/login`

Operacoes `POST`, `PUT` e `DELETE` exigem token JWT.

## Entrega

A colecao minima do Postman esta em:

```text
postman/WebServiceFiap.postman_collection.json
```
