public class Folder
{
    public Folder()
    {
        this.Children = new Dictionary<string, Folder>();
    }

    public required string Name { get; init; }

    public Folder? Parent { get; set; }

    public Dictionary<string, Folder> Children { get; set; }

    public int FileSize { get; set; }

    public int TotalSize
    {
        get
        {
            var total = this.FileSize;

            foreach (var child in this.Children.Values)
            {
                total += child.TotalSize;
            }

            return total;
        }
    }

    public override string ToString()
    {
        return $"{this.Name} - {this.TotalSize}";
    }
}