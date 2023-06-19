namespace top_movie_picks;

public class K_dTree
{
    public Node Root { get; set; }

    public K_dTree(User[] users)
    {
        Root = new Node(users, genre.drama);
    }
/*
    public Node[] FindNeighbours(double range)
    {
        return FindNeighbours(Root, range);
        Node[] FindNeighbours(Node currentNode, double range)
        {
            var genre = currentNode.Genre;
            retur
        }
    }*/
}