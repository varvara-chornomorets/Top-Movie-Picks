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

    public User[] SortUsers(User[] users)
    {
        switch (Genre)
        {
            case genre.drama:
                return users.OrderBy(user => user.drama.average).ToArray();
            case genre.action:
                return users.OrderBy(user => user.action.average).ToArray();
            case genre.animation:
                return users.OrderBy(user => user.animation.average).ToArray();
            case genre.documentary:
                return users.OrderBy(user => user.documentary.average).ToArray();
            case genre.fiction:
                return users.OrderBy(user => user.fiction.average).ToArray();
            case genre.comedy:
                return users.OrderBy(user => user.comedy.average).ToArray();
            case genre.romance:
                return users.OrderBy(user => user.romance.average).ToArray();
            default:
            case genre.thriller:
                return users.OrderBy(user => user.thriller.average).ToArray();
        }
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