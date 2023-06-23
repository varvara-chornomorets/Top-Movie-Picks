namespace top_movie_picks;

public class K_dTree
{
    public static Node Root { get; set; }

    public K_dTree(User[] users)
    {
        Root = new Node(users, genre.drama);
    }

    public User[] FindNeighbours(User user, int number)
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
            var nodeIsNotFarSmaller = user.GetGenre(genre).average - range < nodeUser.GetGenre(genre).average;
            var nodeIsNotFarBigger = user.GetGenre(genre).average + range > nodeUser.GetGenre(genre).average;
            if (nodeIsNotFarBigger && nodeIsNotFarSmaller)
            {
                neighbours.Add(nodeUser);
                if (currentNode.Left != null) neighbours.AddRange(LocalFindNeighbours(currentNode.Left, range, user));
                if (currentNode.Right != null) neighbours.AddRange(LocalFindNeighbours(currentNode.Right, range, user));
            }
            else if (nodeIsNotFarBigger && currentNode.Left != null) neighbours.AddRange(LocalFindNeighbours(currentNode.Left, range, user));
            else if (nodeIsNotFarSmaller && currentNode.Right != null) neighbours.AddRange(LocalFindNeighbours(currentNode.Right, range, user));
            return neighbours.ToArray();
        }
    }
}