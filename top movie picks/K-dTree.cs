namespace top_movie_picks;

public class K_dTree
{
    public static Node Root { get; set; }

    public K_dTree(User[] users)
    {
        Root = new Node(users, genre.drama);
    }

    public static User[] FindNeighbours(User user, int number)
    {
        var range = 0.1;
        var neighbours = LocalFindNeighbours(Root, range, user);
        while (neighbours.Length < number)
        {
            range += 0.1;
            neighbours = LocalFindNeighbours(Root, range, user);
        }

        return neighbours.OrderBy(user.FindDifference).Take(number).ToArray();
        
        User[] LocalFindNeighbours(Node currentNode, double range, User user)
        {
            var genre = currentNode.Genre;
            var nodeUser = currentNode.Value;
            var neighbours = new List<User>();
            if (user.GetGenre(genre).average - range < nodeUser.GetGenre(genre).average && 
                user.GetGenre(genre).average + range > nodeUser.GetGenre(genre).average) neighbours.Add(nodeUser);
            if (currentNode.Left != null) LocalFindNeighbours(currentNode.Left, range, user);
            if (currentNode.Right != null) LocalFindNeighbours(currentNode.Right, range, user);
            return neighbours.ToArray();
        }
    }
}