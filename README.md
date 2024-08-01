# Pokemon API

Este é um projeto de exemplo para listar e capturar Pokémons utilizando a PokeAPI.

## Tecnologias Utilizadas

- C#
- ASP.NET Core
- Entity Framework Core
- SQLite
- Swagger

## Como Executar

1. Clone o repositório
2. Navegue até o diretório do projeto
3. Execute `dotnet restore` para restaurar os pacotes
4. Execute `dotnet run` para iniciar a aplicação

## Endpoints

- `POST /api/masterpokemon`: Cadastra um novo mestre Pokémon
Exemplo de body:
    ```json
    {
        "name": "string",
        "age": 0,
        "cpf": "string"
    }
    ```

- `GET /api/masterpokemon/{id}`: Retorna detalhes de um mestre Pokémon específico

- `POST /api/masterpokemon/capture`: Captura um Pokémon para um mestre específico
  Exemplo de body:
    ```json
    {
        "masterId": 0,
        "pokemonId": 0
    }
    ```
- `GET /api/pokemon/random`: Retorna 10 Pokémons aleatórios
- `GET /api/pokemon/{id}`: Retorna detalhes de um Pokémon específico
- `GET /api/masterpokemon/{masterId}/captured`: Lista todos os Pokémons capturados por um mestre específico

<br />
<hr />
<br />

>  This is a challenge by [Coodesh](https://coodesh.com/)
