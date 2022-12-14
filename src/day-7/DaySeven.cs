/// <summary>
/// https://adventofcode.com/2022/day/7
/// </summary>
public sealed class DaySeven : IDay<int>
{
    /// <summary>
    /// Find all of the directories with a total size of at most 100000.
    /// What is the sum of the total sizes of those directories?
    /// </summary>
    /// <param name="lines">Instructions</param>
    /// <returns>Total size of all the directories together</returns>
    public static int PartOne(string[] lines)
    {
        var maxFolderSize = 100000;
        var folders = FollowInstructions(lines);

        return folders.Where(x => x.TotalSize <= maxFolderSize)
                      .Sum(x => x.TotalSize);
    }

    /// <summary>
    /// Find the smallest directory that, if deleted, would free up enough
    /// space on the filesystem to run the update. What is the total size of that directory?
    /// </summary>
    /// <param name="lines">Instructions</param>
    /// <returns>Total size of the directory that need to be removed</returns>
    public static int PartTwo(string[] lines)
    {
        var totalSpaceDiskDrive = 70000000;
        var requiredSpaceFree = 30000000;
        var freeUpAtLeast = totalSpaceDiskDrive - requiredSpaceFree;

        var folders = FollowInstructions(lines);

        var orderedFolders = folders.OrderByDescending(x => x.TotalSize);
        var totalSpaceClaimed = orderedFolders.First().TotalSize;
        var unusedSpace = totalSpaceDiskDrive - totalSpaceClaimed;
        var reclaimSpace = requiredSpaceFree - unusedSpace;

        return orderedFolders.Where(x => x.TotalSize >= reclaimSpace)
                             .Last()
                             .TotalSize;
    }

    private static List<Folder> FollowInstructions(string[] lines)
    {
        var root = new Folder
        {
            Name = "/",
        };

        var folders = new List<Folder> {
            root
        };

        var currentFolder = root;

        for (var i = 1; i < lines.Length; i++)
        {
            var instruction = lines[i];
            // List folder structure
            if (instruction.Equals("$ ls", StringComparison.InvariantCultureIgnoreCase))
            {
                ListFilesAndFolders(lines, currentFolder, ref i, ref instruction);
                continue;
            }

            if (instruction.Equals("$ cd .."))
            {
                currentFolder = currentFolder.Parent;
                continue;
            }

            if (instruction.StartsWith("$ cd "))
            {
                currentFolder = ChangeFolder(folders, currentFolder, instruction);
            }
        }

        return folders;
    }

    private static void ListFilesAndFolders(string[] lines, Folder? currentFolder, ref int i, ref string instruction)
    {
        // Continue till next command
        while (lines.Length > i + 1 && !lines[i + 1].StartsWith("$"))
        {
            i++;
            instruction = lines[i];
            // Ignore folders in file listing
            if (instruction.StartsWith("dir"))
            {
                continue;
            }
            currentFolder.FileSize += int.Parse(instruction.Split(' ')[0]);
        }
    }

    private static Folder ChangeFolder(List<Folder> folders, Folder fromFolder, string instruction)
    {
        var gotoFolder = instruction.Substring(5);
        if (!fromFolder.Children.ContainsKey(gotoFolder))
        {
            var toFolder = new Folder
            {
                Name = gotoFolder,
                Parent = fromFolder
            };

            fromFolder.Children.Add(gotoFolder, toFolder);
            folders.Add(toFolder);
        }
        fromFolder = fromFolder.Children[gotoFolder];
        return fromFolder;
    }

    public class Folder
    {
        public Folder()
        {
            Children = new Dictionary<string, Folder>();
        }

        public required string Name { get; init; }

        public Folder? Parent { get; set; }

        public Dictionary<string, Folder> Children { get; set; }

        public int FileSize { get; set; }

        public int TotalSize
        {
            get
            {
                var total = FileSize;

                foreach (var child in this.Children.Values)
                {
                    total += child.TotalSize;
                }
                return total;
            }
        }

        public override string ToString()
        {
            return $"{Name} - {TotalSize}";
        }
    }
}