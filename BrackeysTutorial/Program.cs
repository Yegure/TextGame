using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

class Programm
{

    static int maxHunger = 100;
    static int hunger = maxHunger;
    static int apple;
    static int bread;
    static int wheat;
    static int stone = 0;
    static int wood = 0;
    static int xp = 0;
    static int level = 1;
    static int skillPoints = 0;
    static int miningSkill = 1;
    static int foragingSkill = 1;
    static int survivalSkill = 1;
    static int combatSkill = 1;
    static int damage = 5;
    static bool hasAxe = false;
    static bool usedSkillpoint = false;
    static bool isGameOver = false;
    static void Main(String[] args)
    {
        Console.WriteLine("Hallo, Dies ist eine Überlebens Simulation sollte dein Hungerwert auf 0 sinken heißt es Gameover.");




        while (!isGameOver)
        {
            usedSkillpoint = false;


            Console.WriteLine($"\nHunger: {hunger} | XP: {xp} | Level: {level}");
            Console.WriteLine("Was möchtest du tun?");
            Console.WriteLine("1 - Stein sammeln");
            Console.WriteLine("2 - Holz sammeln");
            Console.WriteLine("3 - Axt craften");
            Console.WriteLine("4 - Höhle erkunden");
            Console.WriteLine("5 - Skillpunkte verteilen");
            Console.WriteLine("6 - Spiel beenden");

            if (xp >= 10 * level)
            {
                hunger = 100;
                xp = 0;
                level++;
                skillPoints++;

                Console.WriteLine("Herzlichen Glückwunsch du bist im level aufgestiegen Level: " + level);

            }

            if (hunger == 0)
            {
                isGameOver = true;

                Console.WriteLine("Du bist verhungert viel Glück beim nächsten mal!");
            }


            string answer = Console.ReadLine().ToLower();


            switch (answer)
            {
                case "1":
                    stone++;
                    xp += 3;
                    hunger -= (5 / miningSkill);
                    Console.WriteLine("Du hast soeben ein Stein aufgesammelt dies kostete dich jedoch Hunger außerdem hast du 3 Xp Punkte gesammelt");
                    break;

                case "2":
                    wood++;
                    xp += 3;
                    hunger -= 5 / (foragingSkill / 5);
                    Console.WriteLine("Du hast soeben Holz aufgesammelt dies kostete dich jedoch Hunger außerdem hast du 3 Xp Punkte gesammelt");
                    break;

                case "3":
                    if (wood >= 2 && stone >= 3)
                    {
                        hasAxe = true;
                        stone -= 3;
                        wood -= 2;
                        damage += 5;
                        Console.WriteLine("Du hast soeben eine Axt gecraftet dies kostete dich kein Hunger und nun bekommst du weniger Hunger durch das Sammalen von Holz");

                    }
                    else
                    {
                        Console.WriteLine("Schade... du hast nicht genug Materialien beim sammeln von Holz bekommst du so viel Hunger wie Üblich");
                    }
                    break;

                case "4":
                    Cave();

                    break;

                case "5":

                    if (skillPoints <= 0)
                    {
                        Console.WriteLine("Du hast keine Skillpunkte.");
                        break;
                    }

                    bool skillPointUsed = false;
                    while (!skillPointUsed)
                    {
                        Console.WriteLine($"\nMining: {miningSkill} | Foraging: {foragingSkill} | Combat: {combatSkill} | Survival: {survivalSkill}");
                        Console.WriteLine("Was möchtest du aufleveln?");
                        Console.WriteLine("1 - Mining");
                        Console.WriteLine("2 - Foraging");
                        Console.WriteLine("3 - Combat");
                        Console.WriteLine("4 - Survival");
                        Console.Write("Deine Wahl: ");
                        string skillAnswer = Console.ReadLine();

                        switch (skillAnswer)
                        {
                            case "1":
                                miningSkill++;
                                skillPoints--;
                                skillPointUsed = true;
                                Console.WriteLine("Mining erhöht um 1! dein jetziges Level: " + miningSkill);
                                break;

                            case "2":
                                foragingSkill++;
                                skillPoints--;
                                skillPointUsed = true;
                                Console.WriteLine("Foraging erhöht um 1! dein jetziges Level: " + foragingSkill);
                                break;

                            case "3":
                                combatSkill++;
                                skillPoints--;
                                skillPointUsed = true;
                                Console.WriteLine("Combat erhöht um 1! dein jetziges Level: " + combatSkill);
                                break;

                            case "4":
                                survivalSkill++;
                                skillPoints--;
                                skillPointUsed = true;
                                Console.WriteLine("Survival erhöht! dein jetziges Level: " + survivalSkill);
                                break;

                            default:
                                Console.WriteLine("Ungültige Eingabe. Bitte wähle 1, 2, 3 oder 4.");
                                break;
                        }
                    }

                    break;




                case "6":
                    isGameOver = true;
                    Console.WriteLine("Du hast das Spiel beendet bis zum nächsten mal!");
                    break;




            }



        }
    }

    static void Cave()
    {
        Console.WriteLine("Du hast eine Höhle betreten hier findest du wertvolle Erze jedoch können dich hier Kreaturen erwarten!");
        

    }


}


