# Projeto 3 da Disciplina de Linguagens de Programação 1

## Jogo Roguelike

Marco Domingos, 21901309  
Daniel Fernandes, a21902752
Pedro Bezerra, a21900974  

## Autoria

Nesta secção será indicado exatamente o que cada aluno fez no projeto. Para além
das coisas mencionadas, cada aluno também trabalhou em pequenas partes do
programa dos outros membros do grupo (arranjar bugs, pequenas funcionalidades).

### O que foi feito por Marco Domingos

#### Classe `Game`

A maioria do método `Game Loop()` foi feito completamente por Marco Domingos, 
assim como o método `Initiate()`. Boa parte da documentação e comentários também
foram feitos por Marco Domingos.

#### Classe `UI`

A classe `UI` foi essencialmente toda feita por Marco Domingos, com exceção dos
métodos `WriteMessage()`, `PromptUsername()`, `PromptSaveFile()`, 
`ShowHighscoreTable()`, e `ShowBoard()`.

#### Classe `Board`

A propriedade `CurrentBoard` foi alterada por Marco Domingos.

#### Classe `Player`

O método `WhereToMove()` foi altamente modificado por Marco Domingos (mas não
criado).

#### Classe `Entity`

Método `override ToString()` feito maioritariamente por Marco Domingos.

#### *XML*

Muito da documentação *XML* e comentários foram feitos por Marco Domingos e 
Pedro Bezerra.

#### Relatório

Todo o relatório, com exceção da sua criação inicial (feito por Daniel
Fernandes), foi feito por Marco Domingos.

### O que foi feito por Daniel Fernandes

#### Classe `GameValues`

Daniel Fernandes é responsável por toda a classe `GameValues`, excluindo 
documentação e revisões de comentários.

#### Classe `SaveManager`

Uma parte da componente opcional de *save* e *load* foi totalmente feita por 
Daniel Fernandes.

#### Classe `HighscoreTable`, `Score`

Ambas estas classes foram feitas por Daniel Fernandes.

#### Classe `Board` 
O método `PlaceEntity()`

#### Classe `Game`

A maioria da classe foi feita por Daniel Fernandes, com exceção dos métodos já
mencionados acima.

### *XML*

Daniel fez a maioria dos comentários e *XML* nas suas próprias classes.

### O que foi feito por Pedro Bezerra

#### Enum `Direction`

#### Classe `Board`

Todos os métodos, propriedades, e variáveis aqui foram feitos por Pedro Bezerra,
com exceção dos mencionados acima.

#### Classe `Game`

A maioria do método `GameLoop()` foi editado e refinado por Pedro Bezerra, 
assim como o método `MovePlayer()`

#### Classe `Coord`

Todos os métodos, propriedades, e documentação foram feitos por Pedro Bezerra.

#### Classe `Player`

Todos os métodos foram feitos por Pedro Bezerra.

#### Classe `Enemy`

Todos os métodos foram feitos por Pedro Bezerra.

#### Classe `Entity`

Todos os métodos, com exceção do `override ToString()` foram feitos por Pedro
Bezerra.

### *XML*

Muito da documentação *XML* e comentários foram feitos por Marco Domingos e 
Pedro Bezerra.

## Arquitetura da solução

![flux]

## Referências

Grande parte da lógica do projeto foi baseada no exemplo 
[ZombiesVsHumans](https://github.com/VideojogosLusofona/lp1_2018_p2_solucao)
dado pelo Professor Nuno Fachada, e alguns métodos, como o `ProcGenFunction`,
são totalmente de sua autoria.

[flux]: Diagrams\UML.png