using MuholdApp;

List<Muhold> muholdak = new List<Muhold>();
int hibasSorokSzama = 0;
foreach (var line in File.ReadAllLines("muholdak.txt").Skip(1))
{
    if (string.IsNullOrWhiteSpace(line)) { hibasSorokSzama++; continue; }
    string[] data = line.Split('|');
    if (data.Length != 4) { hibasSorokSzama++; continue; }
    if (data[0] != "M" && data[0] != "K" || string.IsNullOrWhiteSpace(data[1])) { hibasSorokSzama++; continue; }
    if (!int.TryParse(data[2], out int suly) || !int.TryParse(data[3], out int muszerek)) { hibasSorokSzama++; continue; }
    if (data[0] == "M")
    {
        Muhold muhold = new Muhold(data[1], int.Parse(data[2]));
        muholdak.Add(muhold);
    }
    else
    {
        KutatasiMuhold muhold = new KutatasiMuhold(data[1], int.Parse(data[2]), int.Parse(data[3]));
        muholdak.Add(muhold);
    }

}
Console.WriteLine("Sikeresen beolvasott műholdak: {0} db", muholdak.Count());
Console.WriteLine("Kiszűrt hibás sorok száma: {0} db", hibasSorokSzama);

Console.WriteLine("\nBeolvasott műholdak listája:");
foreach(var mu in muholdak) Console.WriteLine(mu);

Console.WriteLine("\nLINQ Statisztikák:");
Console.WriteLine($"Műholdak átlagos tömege: {muholdak.Average(m => m.SulyKg)} kg");
Console.WriteLine($"Legnehezebb műhold: {muholdak.MaxBy(m => m.SulyKg).Azonosito} ({muholdak.MaxBy(m => m.SulyKg).SulyKg} kg)");
Console.WriteLine($"Legalacsonyabb napi költségű műhold: {muholdak.MinBy(m => m.NapiKoltseg()).Azonosito} ({muholdak.MinBy(m => m.NapiKoltseg()).NapiKoltseg()} EUR)");
var tonnasok = muholdak.Where(m => m.SulyKg >= 1000);
Console.WriteLine($"Legalább 1000 kg-os műholdak száma: {tonnasok.Count()} db");

Console.WriteLine("\nKutatási műholdak külön elemzése:");
foreach(var mu in muholdak)
{
    if(mu is KutatasiMuhold ku)
    {
        Console.WriteLine($"- {ku.Azonosito} kutatási műhold műszereinek száma: {ku.MuszerekSzama} db");
    }
}

IEnumerable<string> rawData = muholdak.OrderByDescending(m => m.NapiKoltseg()).Where(m => m.NapiKoltseg() > 2000).Select(m => $"{m.Azonosito};{m.SulyKg};{m.NapiKoltseg()}");
File.WriteAllLines("riport.txt", rawData);
Console.WriteLine("\nA riport.txt fájl sikeresen elkészült a kimeneti mappában.");

Console.WriteLine($"\nMűtárgy példányok száma (static): {Muhold.OsszesMuholdSzama} db");