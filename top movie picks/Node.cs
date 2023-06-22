namespace top_movie_picks;

public class Node
{
    public Node? Left { get; }
    public Node? Right { get; }
    public User Value { get; }
    public genre Genre { get; }
    

    public Node(User[] users, genre genre)
    {
        Genre = genre;
        var middle = users.Length / 2;
        var sortedUsers = SortUsers(users);
        Value = sortedUsers[middle];
        switch (sortedUsers.Length)
        {
            case 1:
                return;
            case > 2:
                Left = new Node(sortedUsers[..middle], NextGenre(Genre));
                Right = new Node(sortedUsers[(middle + 1)..], NextGenre(Genre));
                return;
            default:
                Left = new Node(sortedUsers[..middle], NextGenre(Genre));
                break;
        }
    }

    private User[] SortUsers(User[] users) => 
        users.OrderBy(user => user.GetGenre(Genre).average).ToArray();

    public genre NextGenre(genre genre)
    {
        return genre switch
        {
            genre.drama => genre.comedy,
            genre.comedy => genre.action,
            genre.action => genre.romance,
            genre.romance => genre.fiction,
            genre.fiction => genre.animation,
            genre.animation => genre.thriller,
            genre.thriller => genre.documentary,
            _ => genre.drama
        };
    }
}

public enum genre
{
    drama,
    comedy,
    action,
    romance,
    fiction,
    animation,
    thriller,
    documentary
};