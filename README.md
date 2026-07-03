# 🚗 Exercícios de POO em C#

Repositório de estudos de **Programação Orientada a Objetos (POO)** em C#, com exercícios e pequenos sistemas desenvolvidos para praticar conceitos como herança, polimorfismo, interfaces, encapsulamento e abstração.

> 📚 Este é um repositório de **estudos**. O código está em evolução constante conforme avanço no aprendizado — nem tudo aqui está "perfeito", e tudo bem, esse é o objetivo: aprender na prática.

---

## 📌 Projetos

### Sistema de Locação de Veículos

Um sistema simples de locadora de veículos, simulando cadastro, aluguel e devolução de carros, motos e caminhões.

**Conceitos de POO praticados:**

- **Herança** — `Carro`, `Moto` e `Caminhao` herdam da classe abstrata `Veiculos`
- **Polimorfismo** — cada veículo implementa sua própria regra de `CalcularValorLocacao()`
- **Interfaces** — `IManutencao` define contrato de manutenção para carros e motos
- **Encapsulamento** — a classe `Cliente` valida CPF, nome e CNH através de propriedades com `get`/`set`
- **Abstração** — a classe base `Veiculos` define o comportamento comum a todos os veículos

**Funcionalidades:**

- ✅ Cadastro de veículos na frota
- ✅ Listagem de veículos disponíveis
- ✅ Aluguel de veículo para um cliente
- ✅ Devolução de veículo com geração de recibo
- ✅ Cálculo de valor de locação com regras específicas por tipo de veículo

---

## 🛠️ Tecnologias

- C#
- .NET

## ▶️ Como executar

```bash
dotnet run
```

---

## 🎯 Objetivo do repositório

Este repositório faz parte da minha jornada de aprendizado em programação orientada a objetos. A ideia é praticar conceitos teóricos através de exercícios práticos e, aos poucos, evoluir a qualidade e organização do código.

Sinta-se à vontade para dar sugestões ou apontar melhorias! 🙂

---

## 📄 Licença

Este projeto é apenas para fins de estudo.