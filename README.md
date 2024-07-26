# Gerenciamento de Empregados

Uma aplicação para gerenciar empregados, permitindo a criação, atualização, visualização e exclusão de empregados através de uma API REST e uma aplicação web.

## Implementado

- **Exibir lista de empregados**: Com possibilidade de filtragem (API e aplicação web)
- **Criar empregado**: (API e aplicação web)
- **Atualizar empregado**: (API e aplicação web)
- **Deletar empregado**: (API e aplicação web)

## Não Implementado

- **Mecanismo de autenticação**

## Funcionamento

A comunicação da aplicação web com a API se dá através da URL configurada na variável `urlApi`, dentro do arquivo `app.component.ts`.

## Tecnologias Utilizadas

- **Banco de Dados**: SQLite
- **API**: REST
- **ORM**: Entity Framework Core (EF Core)
- **Back-end**: .NET
- **Front-end**: Angular
