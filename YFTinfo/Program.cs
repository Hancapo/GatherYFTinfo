using System.Diagnostics;
using System.Globalization;
using System.Text;
using CodeWalker.GameFiles;
using SharpDX;


//Set culture to en-US to ensure that floats are formatted with . as decimal separator
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");

//Create timer
Stopwatch sw = new Stopwatch();
sw.Start();

//Creates a list with all the YFT files present in yfts folder in the same folder where the executable is located
List<string> YftFiles = Directory.GetFiles("yfts/", "*.yft").ToList();

//Several lists of some of the unknown values with their respective types and names.
List<ulong> Unknown_100h_list = new();
List<ulong> Unknown_108h_list = new();
List<ulong> Unknown_10h_list = new();
List<ulong> Unknown_128h_list = new();
List<ulong> Unknown_18h_list = new();
List<ulong> Unknown_50h_list = new();
List<ulong> Unknown_70h_list = new();
List<ulong> Unknown_78h_list = new();
List<ulong> Unknown_80h_list = new();
List<ulong> Unknown_88h_list = new();
List<ulong> Unknown_90h_list = new();
List<ulong> Unknown_98h_list = new();
List<ulong> Unknown_A0h_list = new();
List<int> Unknown_B0h_list = new();
List<int> Unknown_B4h_list = new();
List<int> Unknown_B8h_list = new();
List<int> Unknown_BCh_list = new();
List<int> Unknown_C0h_list = new();
List<int> Unknown_C4h_list = new();
List<int> Unknown_C8h_list = new();
List<float> Unknown_CCh_list = new();
List<byte> Unknown_D8h_list = new();
List<ushort> Unknown_DAh_list = new();
List<uint> Unknown_DCh_list = new();
List<ulong> Unknown_E8h_list = new();

List<float> BuoyancyFactor_list = new();

List<Vector3> PLG1_DampingAngularV_list = new();
List<Vector3> PLG1_DampingAngularC_list = new();

List<Vector3> PLG1_DampingLinearV_list = new();
List<Vector3> PLG1_DampingLinearC_list = new();
List<Vector3> PLG1_DampingLinearV2_list = new();

List<Vector3> PLG2_DampingAngularV_list = new();
List<Vector3> PLG2_DampingAngularC_list = new();

List<Vector3> PLG2_DampingLinearV_list = new();
List<Vector3> PLG2_DampingLinearC_list = new();
List<Vector3> PLG2_DampingLinearV2_list = new();

List<Vector3> PLG3_DampingAngularV_list = new();
List<Vector3> PLG3_DampingAngularC_list = new();

List<Vector3> PLG3_DampingLinearV_list = new();
List<Vector3> PLG3_DampingLinearC_list = new();
List<Vector3> PLG3_DampingLinearV2_list = new();

//A paralleled loop to read all the YFT files to load all the data from the YFTs
Parallel.ForEach(YftFiles, YftFile =>
{
    //print progress in percentage

    //Creating a new YFT object
    YftFile yf = new();
    //Loading the YFT file
    yf.Load(File.ReadAllBytes(YftFile));
    //Reading data from each YFT file and adding the values to their respective lists
    Unknown_100h_list.Add(yf.Fragment.Unknown_100h);
    Unknown_108h_list.Add(yf.Fragment.Unknown_108h);
    Unknown_10h_list.Add(yf.Fragment.Unknown_10h);
    Unknown_128h_list.Add(yf.Fragment.Unknown_128h);
    Unknown_18h_list.Add(yf.Fragment.Unknown_18h);
    Unknown_50h_list.Add(yf.Fragment.Unknown_50h);
    Unknown_70h_list.Add(yf.Fragment.Unknown_70h);
    Unknown_78h_list.Add(yf.Fragment.Unknown_78h);
    Unknown_80h_list.Add(yf.Fragment.Unknown_80h);
    Unknown_88h_list.Add(yf.Fragment.Unknown_88h);
    Unknown_90h_list.Add(yf.Fragment.Unknown_90h);
    Unknown_98h_list.Add(yf.Fragment.Unknown_98h);
    Unknown_A0h_list.Add(yf.Fragment.Unknown_A0h);
    Unknown_B0h_list.Add(yf.Fragment.Unknown_B0h);
    Unknown_B4h_list.Add(yf.Fragment.Unknown_B4h);
    Unknown_B8h_list.Add(yf.Fragment.Unknown_B8h);
    Unknown_BCh_list.Add(yf.Fragment.Unknown_BCh);
    Unknown_C0h_list.Add(yf.Fragment.Unknown_C0h);
    Unknown_C4h_list.Add(yf.Fragment.Unknown_C4h);
    Unknown_C8h_list.Add(yf.Fragment.Unknown_C8h);
    Unknown_CCh_list.Add(yf.Fragment.Unknown_CCh);
    Unknown_D8h_list.Add(yf.Fragment.Unknown_D8h);
    Unknown_DAh_list.Add(yf.Fragment.Unknown_DAh);
    Unknown_DCh_list.Add(yf.Fragment.Unknown_DCh);
    Unknown_E8h_list.Add(yf.Fragment.Unknown_E8h);

    BuoyancyFactor_list.Add(yf.Fragment.BuoyancyFactor);

    //I create the nullable object of FragPhysicsLOD to avoid null reference exceptions, and I set a default value of null to each one
    FragPhysicsLOD? PLG1 = null;
    FragPhysicsLOD? PLG2 = null;
    FragPhysicsLOD? PLG3 = null;
    //I check if the YFT file has a Physics LOD Group, therefore, if any is present, I set the value of each FPL to the respective one
    if (yf.Fragment.PhysicsLODGroup != null)
    {
        PLG1 = yf.Fragment.PhysicsLODGroup.PhysicsLOD1;
        PLG2 = yf.Fragment.PhysicsLODGroup.PhysicsLOD2;
        PLG3 = yf.Fragment.PhysicsLODGroup.PhysicsLOD3;
    }

    //Here I check if those Lod groups actually exists, hence if they do, I traverse them to read their data so I can add it to the respective lists
    if (PLG1 != null)
    {
        PLG1_DampingAngularC_list.Add(PLG1.DampingAngularC);
        PLG1_DampingAngularV_list.Add(PLG1.DampingAngularV);
        PLG1_DampingLinearC_list.Add(PLG1.DampingLinearC);
        PLG1_DampingLinearV_list.Add(PLG1.DampingLinearV);
        PLG1_DampingLinearV2_list.Add(PLG1.DampingLinearV2);

    }
    if (PLG2 != null)
    {
        PLG2_DampingAngularC_list.Add(PLG2.DampingAngularC);
        PLG2_DampingAngularV_list.Add(PLG2.DampingAngularV);
        PLG2_DampingLinearC_list.Add(PLG2.DampingLinearC);
        PLG2_DampingLinearV_list.Add(PLG2.DampingLinearV);
        PLG2_DampingLinearV2_list.Add(PLG2.DampingLinearV2);
    }
    if (PLG3 != null)
    {
        PLG3_DampingAngularC_list.Add(PLG3.DampingAngularC);
        PLG3_DampingAngularV_list.Add(PLG3.DampingAngularV);
        PLG3_DampingLinearC_list.Add(PLG3.DampingLinearC);
        PLG3_DampingLinearV_list.Add(PLG3.DampingLinearV);
        PLG3_DampingLinearV2_list.Add(PLG3.DampingLinearV2);
    }


});

//Output the mode of all the lists to a single text file
StringBuilder sb = new();
//This is just string "building" I'm showing the mode of each list, the functions above are LINQ funcs, you should use the same method for everything.
sb.AppendLine("Unknown_100h: " + Unknown_100h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_108h: " + Unknown_108h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_10h: " + Unknown_10h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_128h: " + Unknown_128h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_18h: " + Unknown_18h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_50h: " + Unknown_50h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_70h: " + Unknown_70h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_78h: " + Unknown_78h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_80h: " + Unknown_80h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_88h: " + Unknown_88h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_90h: " + Unknown_90h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_98h: " + Unknown_98h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_A0h: " + Unknown_A0h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_B0h: " + Unknown_B0h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_B4h: " + Unknown_B4h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_B8h: " + Unknown_B8h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_BCh: " + Unknown_BCh_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_C0h: " + Unknown_C0h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_C4h: " + Unknown_C4h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_C8h: " + Unknown_C8h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_CCh: " + Unknown_CCh_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_D8h: " + Unknown_D8h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_DAh: " + Unknown_DAh_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_DCh: " + Unknown_DCh_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("Unknown_E8h: " + Unknown_E8h_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());

sb.AppendLine("PLG1_DampingAngularC: " + PLG1_DampingAngularC_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("PLG1_DampingAngularV: " + PLG1_DampingAngularV_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("PLG1_DampingLinearC: " + PLG1_DampingLinearC_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("PLG1_DampingLinearV: " + PLG1_DampingLinearV_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("PLG1_DampingLinearV2: " + PLG1_DampingLinearV2_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());

sb.AppendLine("PLG2_DampingAngularC: " + PLG2_DampingAngularC_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("PLG2_DampingAngularV: " + PLG2_DampingAngularV_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("PLG2_DampingLinearC: " + PLG2_DampingLinearC_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("PLG2_DampingLinearV: " + PLG2_DampingLinearV_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("PLG2_DampingLinearV2: " + PLG2_DampingLinearV2_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());

sb.AppendLine("PLG3_DampingAngularC: " + PLG3_DampingAngularC_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("PLG3_DampingAngularV: " + PLG3_DampingAngularV_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("PLG3_DampingLinearC: " + PLG3_DampingLinearC_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("PLG3_DampingLinearV: " + PLG3_DampingLinearV_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());
sb.AppendLine("PLG3_DampingLinearV2: " + PLG3_DampingLinearV2_list.GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First());


//Then I finally write everything in a text file
File.WriteAllText("Unknowns.txt", sb.ToString());

//stop timer and output time taken
sw.Stop();
Console.WriteLine("Time taken: " + sw.Elapsed);