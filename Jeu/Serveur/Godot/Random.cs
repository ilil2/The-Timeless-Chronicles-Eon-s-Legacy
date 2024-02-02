using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using Godot;
using HttpClient = System.Net.Http.HttpClient;

namespace JeuServeur;

public class Random
{
    private static int _years = DateTime.Now.Year;
    private static int _month = DateTime.Now.Month;
    private static int _day = DateTime.Now.Day;
    private static DayOfWeek _d = DateTime.Now.DayOfWeek;
    public static int Rand()
    {
        DayMaj();
        GetAPI();
        GetAPI("http://www.infoclimat.fr/public-api/gfs/json?_ll=48.85341,2.3488&_auth=BR8DFAF%2FASNUeVViVyEELQBoDjtZLwkuAX0AYwlsAn9WPVIzVTVQNlU7UC0CLQM1VnsFZg80UmJROlAoAXNTMgVvA28BagFmVDtVMFd4BC8ALg5vWXkJLgFjAGEJZwJ%2FVjBSKFU0UDFVJFAxAi0DKFZkBWIPMFJsUTpQNgFsUzYFYwNlAX0BfFQ9VTJXNQRiAGcOOllnCWIBYQA1CWACYFZhUjNVKFA0VTNQOgI7AzRWZwVmDzJSdVEtUE4BH1MtBSYDJQE3ASVUJlViVzkEZA%3D%3D&_c=e56164cde0042d84fac8885f0d9d034d","Meteo.json");
        Console.WriteLine(GetPriceBourse()+GetMeteo());
        return GetPriceBourse() + GetMeteo();
    }

    private static void GetAPI(string apiUrl = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=UBI.PA&interval=30min&apikey=UJE7SEQZQ9UFTJO8&datatype=json", string fileName = "Bourse.json")
    {
        StreamWriter sw = new StreamWriter(fileName);
        
        HttpClient client = new HttpClient();

        try
        {
            // Effectuer la requête GET
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            
            if (response.IsSuccessStatusCode)
            {
                // Récupérer le corps de la réponse
                string responseBody = response.Content.ReadAsStringAsync().Result;
                sw.Write(IndentJson(responseBody));
            }
            else
            {
                Console.WriteLine($"La requête a échoué avec le code : {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
        }
        finally
        {
            if (client != null)
            {
                client.Dispose();
            }
        }
        sw.Close();
    }
    
    private static string IndentJson(string json)
    {
        JsonDocument jsonDocument = JsonDocument.Parse(json);
        return JsonSerializer.Serialize(jsonDocument.RootElement, new JsonSerializerOptions { WriteIndented = true });
    
    }
    
    private static int GetPriceBourse()
    {
        StreamReader sr = new StreamReader("Bourse.json");
        string json = sr.ReadToEnd();
        JsonDocument jsonDocument = JsonDocument.Parse(json);
        JsonElement root = jsonDocument.RootElement;
        JsonElement timeSeries = root.GetProperty("Time Series (Daily)");
        JsonElement today = timeSeries.GetProperty($"{_years}-{_month.ToString().PadLeft(2,'0')}-{_day.ToString().PadLeft(2,'0')}");
        JsonElement closemarcket = today.GetProperty("4. close");
        JsonElement openmarket = today.GetProperty("5. volume");

        return (int)float.Parse(closemarcket.GetString(),CultureInfo.InvariantCulture) * int.Parse(openmarket.GetString());
    }

    private static int GetMeteo()
    {
        StreamReader sr = new StreamReader("Meteo.json");
        string json = sr.ReadToEnd();
        JsonDocument jsonDocument = JsonDocument.Parse(json);
        JsonElement root = jsonDocument.RootElement;
        JsonElement today = root.GetProperty($"{DateTime.Now.Year}-{DateTime.Now.Month.ToString().PadLeft(2,'0')}-{DateTime.Now.Day.ToString().PadLeft(2,'0')} 22:00:00");
        JsonElement temp2m = today.GetProperty("temperature").GetProperty("2m");
        JsonElement nivmer = today.GetProperty("pression").GetProperty("niveau_de_la_mer");
        JsonElement pluie = today.GetProperty("pluie");
        JsonElement ventmoy = today.GetProperty("vent_moyen").GetProperty("10m");

        int n = 0;
        if ((int)(temp2m.GetDouble()*pluie.GetDouble()) == 0)
        {
            n = 1;
        }
        return nivmer.GetInt32() / (int)(temp2m.GetDouble()*pluie.GetDouble()+n) * (int)ventmoy.GetDouble();
    }

    private static void DayMaj()
    {
        _years = DateTime.Now.Year;
        _month = DateTime.Now.Month;
        DayOfWeek d = Yesterday(DateTime.Now.DayOfWeek);
        _day = DateTime.Now.Day-1;
        if (d == DayOfWeek.Sunday)
        {
            _day -= 2;
        }
        else if (d == DayOfWeek.Saturday)
        {
            _day -= 1;
        }
        
        if (_day <= 0)
        {
            _month -= 1;
            if (_month <= 0)
            {
                _years -= 1;
                _month = 12;
            }

            _day = Maxday(_month) + _day;
        }
    }

    private static int Maxday(int month)
    {
        switch (month)
        {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                return 31;
            case 2:
                return 28;
            default:
                return 30;
        }
    }

    private static DayOfWeek Yesterday(DayOfWeek d)
    {
        switch (d)
        {
            case DayOfWeek.Monday:
                return DayOfWeek.Sunday;
            case DayOfWeek.Tuesday:
                return DayOfWeek.Monday;
            case DayOfWeek.Wednesday:
                return DayOfWeek.Tuesday;
            case DayOfWeek.Thursday:
                return DayOfWeek.Wednesday;
            case DayOfWeek.Friday:
                return DayOfWeek.Thursday;
            case DayOfWeek.Saturday:
                return DayOfWeek.Friday;
            default:
                return DayOfWeek.Saturday;
        }
    }
}