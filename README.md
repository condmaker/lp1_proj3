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

Esta secção irá exemplificar como o programa foi estruturado e como funciona.
Primeiramente irá ser explicado como o *loop* do programa funciona, com um
fluxograma para ajudar a compreensão.

![flux]
  
Quando o programa se inicia, ele verifica **opcões da linha de comandos**, que
serão o tamanho de linhas/colunas do tabuleiro, ou a leitura de um ficheiro
de um jogo já salvo (com o sistema de *save*/*load* em `GameValue` e 
`SaveManager`, **em modo de jogo, pois este também trata de *save*/*load* de 
*Highscores*).

Ele então entra no *main menu loop*, na classe `Game`e dá várias opções ao 
jogador-- se este quer ver a tabela de *Highscores*, os créditos de jogo, um 
curto tutorial, ou se quer iniciar o jogo em si. Todas as opções sem ser a de 
iniciar um novo jogo irão retornar ao *loop*. 

Caso o jogador criar um novo jogo (ou automaticamente entra quando dá *load* 
duma file), o programa irá instanciar uma nova `Board` e suas entidades com
o método `GenerateLevel()`. Ele irá depois entrar no método `GameLoop()` e 
iniciar o jogo.
Primeiro ele sempre irá começar com o turno do jogador, utilizando a classe
`static UI`. A classe é `static` pois muitos dos seus métodos serão sempre
utilizados, e propriedades como `Input` são universais. É utilizado, então,  
`UI.ShowCurrentInfo()` seguido de `UI.ShowBoard()`, que irá demonstrar o nível,
a vida do jogador, e o turno atual (será *Player Phase* neste caso),
junto com o método `UI.ShowBoardInstructions()` que imprime instruções básicas
de movimentação seguido de uma legenda do que é o que no tabuleiro.

O jogo então verifica o *input* do jogador, e o move. Caso o *input* não seja
reconhecido ou a direção seja inválida, ele dá uma mensagem de erro, e retorna
a opção para o jogador dar novamente aquele *input*.
Caso o jogador tenha ganho ou tenha morrido, é verificado ai e guardado na 
variável de método `playerWon`.
O processo é então repetido mais uma vez, visto que o jogador pode mover-se duas
vezes por turno. 

![flux_en]

É então o turno dos inimigos, e todos os inimigos daquele tabuleiro são iterados
a partir de uma lista já criada, e utilizam a sua *AI* (na classe `Enemy`, no
método `WhereToMove()`, onde toda a AI está estruturada) para moverem-se para
perto do jogador. Após cada inimigo mover-se, é verificado se o jogador ainda 
está vivo, caso sim, ele volta ao inicio do loop, caso não, `playerWon` devolve
`false`.

### Caminhos de `playerWon`

Utilizado em abundância no código, `playerWon` é um booleano que verifica se 
o jogador ganhou ou não. Em seus ramos, caso ele seja `true` (jogador ganhou), 
o nível (ou *floor*) é iterado e a `Board` instanciada é atualizada, com os 
novos parâmetros do nível seguinte, feito no método de `Game` nomeado 
`NewGame()`, e utiliza `SaveProgress()` para verificar se o jogador deseja 
salvar seu progresso. Caso contrário, o jogo entra em `EndGame()`, que irá 
apontar um `Score` do jogador no método `HighscoreTable`, caso ele esteja nos
*Top 10* daquele computador em específico. Após isto, o jogador é retornado pro
Main Menu.

### Descrição de ficheiros

Esta secção irá brevemente descrever o que é que cada classe/enumerado no 
programa faz. Está aqui um diagrama *UML* representando a estrutura das 
classes do programa.

![UML]

**Este diagrama é apenas uma versão simplificada, demonstrando as dependências
mais importantes**

#### Classe `Entity`

A classe base para todo objeto no tabuleiro de jogo. Contem uma posição `Pos`
(da `Board` em que ele estará), e um enum `EntityKind` que diz que tipo de 
entidade é que ele é (*Player*, *Enemy*, etc...). Também contém um método
`override ToString()` para ser mais fácil de imprimir os símbolos das entidades
no tabuleiro.

#### Classe Abstrata `Agent`

Uma classe abstrata que herda de Entity, e define todos os seres que conseguem
se mover no jogo (`Enemy` e `Player`)-- define métodos `WhereToMove()`, que 
em `Player` será em relação ao `Input` e em `Enemy` em relação a uma *AI*.

#### Classe `Player`

Herda de `Agent`, contém todos os dados necessários do jogador. Adiciona vida
na propriedade `Health`, um método para tomar dano `Damage`, e um método para
cura `Heal` (com *PowerUps*).

#### Classe `Enemy`

Herda de `Agent`, e contém apenas um método novo para verificar se este inimigo
está ao lado dum jogador em `AdjacentToPlayer()`.

#### Classe `Board`

Controla a instância dum tabuleiro e suas peculiaridades. Tem uma propriedade 
que é um *array* de `Entity` (o tabuleiro em sí), nomeado `CurrentBoard`, 
uma lista de *PowerUps* 'escondidos' (utilizados quando um inimigo fica 'em 
cima' dum *PowerUp*, restaurando ele a posição quando tal inimigo sai do local)
com `Entity` nomeada `hiddenPowerUps`. Além disso contém a altura (`Height`) e
largura (`Width`) como propriedades. 
Esta classe tem vários métodos, como um para apanhar a referência de uma 
entidade numa certa `Pos` (`GetEntityAt()`), uma para verificar se certa 
entidade está nos "confinamentos" (altura e largura) do tabuleiro, etc... Mais
informações na documentação.

#### Classe `Coord`

Controla todas as posições (normalmente mencionadas aqui como `Pos`) de jogo.
Tem propriedades `x` e `y`, assim como operadores e um método 
`override ToString()` para imprimir a posição mais facilmente para o jogador na
classe `UI`.

#### Classe estática `UI`

Uma classe estática que controla toda impressão para a consola e *inputs* de 
jogador. Contém métodos `void` para o tutorial, o menu principal, e todos os 
elementos gráficos de jogo. Mais informações na documentação.

#### Enum `EntityKind`

Um enumerado que identifica todos os tipos de entidade (objetos) no jogo-- 
entre agentes como *Player* e *Enemy* para *PowerUps*. Também contém `Undefined`
que é para demonstrar que aquilo **não** é uma entidade e `None` para demonstrar
que não é nenhuma entidade lá.

#### Enum `Direction`

Um símples enumerado que irá identificar se uma entidade foi para cima (`Up`),
baixo (`Down`), esquerda (`Left`) ou direita (`Right`). Também tem uma opção 
`None`, caso a entidade não se mova no contexto. É interpretada no método 
`GetNeighbour()` da classe `Board` e convertida em posições de `Coord`.

#### Classe `GameValues`

Esta classe irá guardar todas as informações importantes de jogo que tem de
ser guardadas para próximos níveis e futuras jogatinas (feitas com o sistema
de *save*/*load* no modo de jogo). Estas informações são, por exemplo, vida do 
jogador (propriedade `Hp`), altura e largura do tabuleiro (propriedades 
`Height` e `Width`), número do nível/*floor* (propriedade `Level`), etc...
Também é onde é calculado o número de inimigos/powerups por nível, com o 
método `static Logistic()` da classe `ProcGenFunction`. 
Também é responsável porconverter os argumentos da linha de comandos quando é 
invocado inicialmente em `Program`, com o método `static ConvertArgs()`.

#### Classe `HighscoreTable`

Uma classe onde uma instância irá guardar uma tabela `Highscore` para aquele
computador, numa variável de instância que é uma lista de `Score`s. Esta tabela 
é salva num ficheiro 'scores.txt', e se não for encontrada outra é criada.
Têm métodos para adicionar `Scores` a tabela (`AddScore()`), obter um score
da lista (`GetScore()`), e observar se um score específico está no *Top 10*, que
é a lista de *Scores* (`IsHighscore()`).

#### Classe `Score`

`Score` implementa a interface `IComparable`, para conseguir comparar-se com si
mesma. Contém uma propriedade `string Name` que é o nome do jogador, e uma
`int NewScore` que será o número de pontos, o *score* em si. 

#### Classe `SaveManager` 

Classe responsável por *save*/*load* de `HighscoreTable`s (para a tabela de 
*Highscores* universal, modo de tabela) e de `GameValues` (para os valores 
importantes de progressão dum jogador, modo de jogo).

## Referências

Grande parte da lógica do projeto foi baseada no exemplo 
[ZombiesVsHumans](https://github.com/VideojogosLusofona/lp1_2018_p2_solucao)
dado pelo Professor Nuno Fachada, e alguns métodos, como o `ProcGenFunction`,
são totalmente de sua autoria. 

Em `coord` também foi utilizada extensivamente a Documentação oficial do C# pela
*Microsoft*, [aqui](link1). Também foi utilizado uma resposta no fórum de 
dúvidas *StackOverflow* para remover items utilizado em `RestorePowerUp()` da
classe `Board`, [aqui](link2). A última referência foi para a realização do 
método `ConvertArgs()` na classe `GameValues`, também apanhado da documentação
oficial do C#, [aqui](link3).


[link1]: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading
[link2]: https://stackoverflow.com/questions/1582285/how-to-remove-elements-from-a-generic-list-while-iterating-over-it
[link3]: https://docs.microsoft.com/en-us/dotnet/api/system.string.replace?view=netcore-3.1

[flux]:    Diagrams/Flowchart.png 
[flux_en]: Diagrams/FlowchartEnemy.png
[UML]:     Diagrams/UML.png 