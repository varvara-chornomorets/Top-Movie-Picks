namespace top_movie_picks;

public class Node
{
    public User[]? Users { get; set; }
    public Node? Left { get; set; }
    public Node? Right { get; set; }
    public genre Genre { get; set; }
    

    public Node(User[] users, genre genre)
    {
        if (users.Length <= 10)
        {
            Users = users;
            return;
        }

        var dividedUsers = DividedUsers(users);
        Genre = genre;
        Left = new Node(dividedUsers[0], NextGenre(Genre));
        Right = new Node(dividedUsers[1], NextGenre(Genre));
    }

    public User[][] DividedUsers(User[] users)
    {
        // Sort by dimension
        var middle = users.Length / 2;
        User[]? sortedUsers;
        switch (Genre)
        {
            case genre.drama:
                sortedUsers = users.OrderBy(user => user.drama.average).ToArray();
                break;
            case genre.action:
                sortedUsers = users.OrderBy(user => user.action.average).ToArray();
                break;
            case genre.animation:
                sortedUsers = users.OrderBy(user => user.animation.average).ToArray();
                break;
            case genre.documentary:
                sortedUsers = users.OrderBy(user => user.documentary.average).ToArray();
                break;
            case genre.fiction:
                sortedUsers = users.OrderBy(user => user.fiction.average).ToArray();
                break;
            case genre.comedy:
                sortedUsers = users.OrderBy(user => user.comedy.average).ToArray();
                break;
            case genre.romance:
                sortedUsers = users.OrderBy(user => user.romance.average).ToArray();
                break;
            default:
            case genre.thriller:
                sortedUsers = users.OrderBy(user => user.thriller.average).ToArray();
                break;
        }
        // Get first half
        // Get second half
        return new[]
        {
            sortedUsers.Take(middle).ToArray(),
            sortedUsers.Skip(middle).ToArray()
        };
    }

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