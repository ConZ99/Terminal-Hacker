using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Timers;
using System.Threading.Tasks;

public class Hacker : MonoBehaviour
{
    System.Random rnd = new System.Random();
    enum Screen {MainMenu, Password, Win }
    int placesNo = places.Count;
    Screen currentScreen;
    string password;
    int choice;

    string[] jokes =
        { "My wife is really mad at the fact that I have no sense of direction. So I packed up my stuff and right.",
          "How do you make holy water? You boil the hell out of it.",
          "I bought some shoes from a drug dealer. I don't know what he laced them with, but I was tripping all day!",
          "I'm reading a book about anti-gravity. It's impossible to put down!",
          "What do you call someone with no body and no nose? Nobody knows.",
          "What noise does a 747 make when it bounces? Boeing, Boeing, Boeing.",
          "You know what the loudest pet you can get is? A trumpet."
        };
    static Dictionary<int, string> places = new Dictionary<int, string>()
    {
        {1, "The Death Star"},
        {2, "Starbucks of Doom"},
        {3, "Mosu's Server" }
    };
    static Dictionary<int, string[]> passwords = new Dictionary<int, string[]>()
    {
        {1, new string[]{"stormtrooper", "wookie", "anakin", "palpatine", "jedi", "sith"}},
        {2, new string[]{"barista", "mochaccino", "cappucino", "overpriced", "coffee", "crowded"}},
        {3, new string[]{"projects", "movies", "music", "games", "anime", "homework"}}
    };

    private void OnUserInput(string input)
    {
        if (input.Equals("menu"))
        {
            ShowMainMenu();
        }
        else if (input.Equals("quit") || input.Equals("close") || input.Equals("exit"))
        {
            Application.Quit();
        }
        else if (input.Equals("Tell me a joke") || input.Equals("tell a joke") || input.Equals("tell me a joke"))
        {
            Terminal.ClearScreen();
            Terminal.WriteLine(jokes[rnd.Next(0, jokes.Length)]);
        }
        else if (input.Equals("say a joke"))
        {
            Terminal.ClearScreen();
            Terminal.WriteLine("A joke! ;)");
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
        
    }

    public void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Hacking options: \n"+
                           "1. " + places[1] + "\n"+
                           "2. " + places[2] + "\n" +
                           "3. " + places[3] + "\n");
        Terminal.WriteLine("It's time to chose a path.\n");
    }
    
    public void RunMainMenu(string input)
    {
        var isNumeric = int.TryParse(input, out choice);
        if (isNumeric && choice <= placesNo)
        {
            Terminal.ClearScreen();
            Terminal.WriteLine("You choose " + places[choice] + "\n");
            AskForPassword();
        }
        
        else
        {
            Terminal.WriteLine("Choose a valid place fam, really!\n" +
                               "You can always choose to the menu.");
        }
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        if (choice <= placesNo)
        {
            string[] aux = passwords[choice];
            password = aux[rnd.Next(0, aux.Length)];
        }
        Terminal.WriteLine("Password hint: " + password.Anagram() + "\nEnter password: ");
    }

    void CheckPassword(string input)
    {
        if(input == password)
        {
            DisplayWinScreen();
        }
        else if (input == "refresh" || input == "f5")
        {
            currentScreen = Screen.Password;
            Terminal.ClearScreen();
            Terminal.WriteLine("Password hint: " + password.Anagram() + "\nEnter password: ");
        }
        else
        {
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        Terminal.WriteLine("WOOOOOOOOOOOOOOOOOOOOOOOOOO");
    }

    void Start()
    {
        ShowMainMenu();
    }
}