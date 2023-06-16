namespace top_movie_picks;

public class K_dTree
{
    public Node Root { get; set; }

    public K_dTree(User[] users)
    {
        Root = new Node(users, genre.drama);
    }
}