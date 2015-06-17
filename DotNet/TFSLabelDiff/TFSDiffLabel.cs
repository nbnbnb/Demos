namespace tfsDiffLabel
{
    using System;
    using System.Collections.Generic;

    using Microsoft.TeamFoundation.Client;
    using Microsoft.TeamFoundation.VersionControl.Client;
    using Microsoft.TeamFoundation.Framework.Client;
    using System.Net;

    class tfsDiffLabel
    {
        #region Fields

        private static VersionControlServer _vcs;

        #endregion Fields

        #region Methods

        static void Main(string[] args)
        {
            if (args.Length != 5)
            {
                Console.WriteLine("Displays differences between two labels within TFS.\n\r");
                Console.WriteLine("TFSLabelDiff [ServerName] [LabelScope] [BaseLabel] [TargetLabel] [Delimiter]\n\r");
                Console.WriteLine("ServerName - The name or URL of the server.");
                Console.WriteLine("LabelScope - The database path that indicates the scope at which the label is defined.");
                Console.WriteLine("BaseLabel - Base label for comparison.");
                Console.WriteLine("TargetLabel - Target label for comparison.");
                Console.WriteLine("Delimiter - String used to delimit output.");
                Environment.Exit(1);
            }

            string sServerName = args[0];
            string sLabelScope = args[1];
            string sBaseLabel = args[2];
            string sTargetLabel = args[3];
            string sDelimiter = args[4];

            try
            {

                 NetworkCredential identity = new NetworkCredential("ZhangJin", "123456");
                using (var tfs = new TfsTeamProjectCollection(new Uri(sServerName), identity))
                {
                    
                    // 身份验证
                    tfs.Authenticate();

                    _vcs = tfs.GetService<VersionControlServer>();

                    var sourceSpec = new LabelVersionSpec(sBaseLabel, sLabelScope);
                    var targetSpec = new LabelVersionSpec(sTargetLabel, sLabelScope);

                    var deltaChangesets = new List<Changeset>();

                    foreach (Changeset changeSet in
                        _vcs.QueryHistory(
                                    path: sLabelScope,
                                    version: targetSpec,
                                    deletionId: 0,
                                    recursion: RecursionType.Full,
                                    user: string.Empty,
                                    versionFrom: sourceSpec,
                                    versionTo: targetSpec,
                                    maxCount: int.MaxValue,
                                    includeChanges: true,
                                    slotMode: false,
                                    includeDownloadInfo: false,
                                    sortAscending: true
                                )
                        )
                        deltaChangesets.Add(changeSet);

                    Console.WriteLine("Code Changes between {0} and {1}", sBaseLabel, sTargetLabel);
                    foreach (Changeset changeSet in deltaChangesets)
                        Console.WriteLine("{0}{1}{2}{1}{3}", changeSet.ChangesetId, sDelimiter, changeSet.Committer, changeSet.Comment.Replace("\n", " ").Replace("\r", " "));

                    Console.WriteLine();

                    Console.WriteLine("ChangesetId{0}Committer{0}Comment", sDelimiter);
                    foreach (Changeset changeSet in deltaChangesets)
                        foreach (Change change in changeSet.Changes)
                            if (change.ChangeType.HasFlag(ChangeType.Add) ||
                                change.ChangeType.HasFlag(ChangeType.Edit) ||
                                change.ChangeType.HasFlag(ChangeType.Delete) ||
                                change.ChangeType.HasFlag(ChangeType.Rename) ||
                                change.ChangeType.HasFlag(ChangeType.SourceRename) ||
                                change.ChangeType.HasFlag(ChangeType.Undelete) ||
                                (change.ChangeType.HasFlag(ChangeType.Branch) && change.ChangeType.HasFlag(ChangeType.Merge)))
                                Console.WriteLine("{0}{1}{2}{1}{3}", changeSet.ChangesetId, sDelimiter, change.Item.ServerItem, change.ChangeType.ToString().Replace(",", ";"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            Environment.Exit(0);
        }

        #endregion Methods
    }
}