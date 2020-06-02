using System;

namespace Roguelike
{
    /// <summary>
    /// Representação de um nível de jogo.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// Variável de instância privada que contém referência ao array 
        /// bi-dimensional que representa o nível de jogo.
        /// </summary>
        private Entity[,] board;

        /// <summary>
        /// Cria uma nova instância do nível de jogo.
        /// </summary>
        /// <param name="height">Dimensão horizontal do nível.</param>
        /// <param name="width">Dimensão vertical do nível.</param>
        public Board(int height, int width)
        {
            board = new Entity[height,width];     
        }

        /// <summary>
        /// Propriedade que representa a dimensão horizontal do nível.
        /// </summary>
        /// <value>Dimensão horizontal do nível.</value>
        public int Width => board.GetLength(0);

        /// <summary>
        /// Propriedade que representa a dimensão vertical do nível.
        /// </summary>
        /// <value>Dimensão vertical do nível.</value>
        public int Height => board.GetLength(1);

        /// <summary>
        /// Método que retorna a entidade de uma posição indicada no 
        /// parâmetro <param name="pos">.
        /// </summary>
        /// <param name="pos">Posição no mundo de jogo.</param>
        /// <returns>Entidade na posição indicada no parâmetro 
        /// <param name="pos">.
        /// </returns>
        // TODO : IEntity?
        public Entity GetEntityAt(Coord pos)
        {       
            pos = Normalize(pos);
            return board[pos.x, pos.y];
        } 

        /// <summary>
        /// Método que indica se existe uma entidade na posição indicada no 
        /// parâmetro <param name="c">
        /// </summary>
        /// <param name="c">Posição no nível.</param>
        /// <returns><c>true</c> se existir uma entidade na posição dada em
        /// <param name="c">, <c>false</c> caso contrário.
        /// </returns>
        public bool IsOccupied(Coord c)
        {
            c = Normalize(c);
            // devolver false se não tiver nada na coordenada
            // devolver true caso contrário.
            return board[c.x, c.y] != null;
        }  

        /// <summary>
        /// Método que devolve a posição vizinha da posição dada no parâmetro
        /// <param name="coord"> na direção do parâmetro 
        /// <param name="direction">.
        /// </summary>
        /// <param name="coord">Posição do nível.</param>
        /// <param name="direction">Direção.</param>
        /// <returns>Posição vizinha da posição dada no parâmetro
        /// <param name="coord"> na direção do parâmetro 
        /// <param name="direction">.
        /// </returns>
        public Coord GetNeighbor(Coord coord, Direction direction)
        {
            // declaração da coordenada de vizinhança.
            Coord neighbor;

            // Verificar a direção e adquirir a coord correspondente.
            switch(direction)
            {
                case Direction.Up:
                    neighbor = coord - new Coord(0, -1);
                    break;
                case Direction.Right:
                    neighbor = coord - new Coord(1, 0);
                    break;
                case Direction.Down:
                    neighbor = coord - new Coord(0, 1);
                    break;
                case Direction.Left:
                    neighbor = coord - new Coord(-1, 0);
                    break;
                default:
                    throw new System.ComponentModel.InvalidEnumArgumentException
                    ("Direção não reconhecida.");
            }
            neighbor = Normalize(neighbor);
            return neighbor;
        }

        /// <summary>
        /// Move entidade <param name="entity"> para a posição 
        /// <param name="coord">
        /// </summary>
        /// <param name="entity">Entidade a ser movida</param>
        /// <param name="coord">Coordenada de destido da Entidade</param>
        public void MoveEntity(Entity entity, Coord coord)
        {
            coord = Normalize(coord);
            board[coord.x, coord.y] = entity;
            board[entity.Pos.x, entity.Pos.y] = null;
        }

        /// <summary>
        /// normaliza coordenada, garantindo que esta está dentro dos limites 
        /// do nível
        /// </summary>
        /// <param name="c">Coordenada a normalizar.</param>
        /// <returns>Coordenada normalizada.</returns>
        public Coord Normalize(Coord c)
        {
            // inicialmente os valores originais
            int x = c.x;
            int y = c.y;
            
            // trata da dimensão horizontal
            while (x>= Width) x -= Width;
            while (x < 0) x += Width;

            // trata da dimensão vertical
            while (y>= Height) y -= Height;
            while (y < 0) y += Height;

            return new Coord(x, y);
        }

    }
}