using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
// To do list:
//-Skills (Wählbar wie in einem RPG)
// = Ultimate Sight: Macht es möglich in einem Höhlen gang anstatt 3 erze 5 zu sehen (I = 5, II = 6,III = 7, IV = 8, V = 10)
// = Gatherer: Erhöht die drop rate (I = doppel drop 10%, II dp 50%, III dp 100% tp 5%, IV tp 15%, V tp 30% qp 3%)
// = Miner: Erhöht die drop rate von Erzen und reduziert hunger kosten(I = dp 2% hr 1, II = dp 4% hr 2, III = dp 6% hr 4, IV = dp 8% hr 4, V dp 10% tp 2% hr 6 )
//-Shop
// Erze und Sammel Ressourcen sollen verkaufbar sein und die möglichkeit eröffnen fortgeschrittene Rohstoffe, Nahrung und sinnvolle Items zu Kaufen
//-Kampf
// -Waffen (50% done!) (Blancing muss noch gemacht werden lol)
//  = Dolch: soll im Kampf die Axt ersetzen und erhört den schaden um 3 damage
//  = Rostiges Schwert: soll in Höhlen gefunden werden (selten) soll ein upgrade des Dolches sein und erhöht den schaden um 6 damage
//  = Glänzendes Schwert: Upgrade zum Rostigem Schwert und wird aus 5 Eisen, 10 Kohle, einem Diamant und ein Rostiges Schwert gecraftet
//  = Schimmerndes Schwert: Upgrade zum vorherigen Schwert wird aus 10 Gold, 20 Eisen, 3 Diamanten, einem Rubin und dem gz Schwert gecraftet
//  = Schwert der Legenden: das Letzte freischaltbare schwert und kann gecraftet werden (Rezept nicht ausgedacht) jedoch muss man Combat skill 10 sein und den Drachen getötet haben.
//  Es soll ebenfalls möglich sein Waffen zu kaufen welche teils stärker sind als die Schwerter und besondere stats haben
//-Gegner
// In Höhlen wird es öfter passieren das der Spieler auf Gegner trifft
// - Scaling
// = Leben: Leben sind basierend auf dem survival Skill des gegners (enemySurvivalSkill = survivalSkill * 2 + rarity) und der Seltenheit des gegners
// = Schaden: der Schaden wird genau so gescaled wie die Leben des gegners
// - Arten
// = Standart: diese Rarität ist die häufigste (Skelette, Zombies, Spinnen, Goblins)
// = Ritter: 2. häufigste (Skelett Ritter, Zombie Ritter, Goblin Krieger)
// = Kommandanten: seltene (Skelett Kriegsherr, Zombie Kommandant, Goblin Strategist)
// = Bosse: epische seltenheit (Gefallener krieger der Antike, Der Drache, Zombie Tyrannt, Schwarzer Ritter der Skellette) diese Bosse werden ähnlich scalen jedoch viel mehr schaden machen und viel mehr leben haben
// = Legenden: legendäre Kreaturen (Hydra, Wyvern, ...) Gegner welche schwer und selten sind jedoch items und ressourcen geben
// = Götter: Göttliche Kreaturen (Hades, Kronos, Starving Grace ...)Dies sind die gegner welche man besiegen muss um das Spiel zu beenden es sollen insgesamt 6 sein welche jeweils ihre eigenen stärken und schwächen haben.
//-Dorf
// 

class Programm
{
    //Leben
    static int hunger = 100;
    //Wie viele Erze Spawnen können
    static int sight = 3;
    static int apple = 0;
    static int bread = 0;
    static int wheat = 0;
    static int stone = 0;
    static int coal = 0;
    static int iron = 0;
    static int gold = 0;
    static int diamond = 0;
    static int ruby = 0;
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
    static bool hasDagger = false;
    static bool hasRustySword = false;
    static bool hasShinySword = false;
    static bool hasShimmeringSword = false;
    static bool hasSwordOfLegends = false;
    static bool usedSkillpoint = false;
    static bool isGameOver = false;
    static void Main(String[] args)
    {
        Console.WriteLine("Hallo, Dies ist eine Überlebens Simulation sollte dein Hungerwert auf 0 sinken heißt es Gameover.");




        while (!isGameOver)
        {
            usedSkillpoint = false;



            if (survivalSkill >= 2)
            {
                hunger = hunger + (survivalSkill * 5);
            }

            if (combatSkill >= 2)
            {
                damage = damage + (combatSkill * 2);
            }



            Console.WriteLine($"\nHunger: {hunger} | XP: {xp} | Level: {level} | Damage : {damage}");
            Console.WriteLine("Was möchtest du tun?");
            Console.WriteLine("1 - Stein sammeln");
            Console.WriteLine("2 - Holz sammeln");
            Console.WriteLine("3 - Craften");
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
                    bool crafted = false;
                    while (!crafted)
                    {
                        Console.WriteLine($"\nWood: {wood} | Stone: {stone} | Coal: {coal} | Iron: {iron} | Gold: {gold} | Diamond: {diamond}; ");
                        Console.WriteLine("Was möchtest du Craften?");
                        Console.WriteLine("1 - Axe");
                        Console.WriteLine("2 - Dagger");
                        Console.WriteLine("3 - Shiny Sword");
                        Console.WriteLine("4 - Shimmering Sword");
                        Console.WriteLine("5 - Sword of Legends");
                        Console.WriteLine("6 - Verlassen");
                        string craftAnswer = Console.ReadLine();

                        switch (craftAnswer)
                        {
                            case "1":
                                if (wood >= 2 && stone >= 3)
                                {
                                    hasAxe = true;
                                    wood -= 2;
                                    stone -= 3;
                                    Console.WriteLine("Du hast soeben eine Axt gecrafted welche dir weniger hunger beim abbauen von Holz kostet!");
                                    crafted = true;

                                }
                                else
                                {
                                    Console.WriteLine("Dir fehlen die nötigen Ressourcen!");
                                }
                                break;

                            case "2":
                                if (wood >= 4 && iron >= 2)
                                {
                                    hasDagger = true;
                                    wood -= 4;
                                    iron -= 2;
                                    Console.WriteLine("Du hast soeben einen Dolch gecraftet welcher deinen Schaden leich erhöht");
                                    crafted = true;
                                }
                                else
                                {
                                    Console.WriteLine("Dir fehlen die nötigen Ressourcen!");
                                }
                                break;

                            case "3":
                                if (hasRustySword = true && iron >= 5 && coal >= 10 && diamond >= 1)
                                {
                                    hasShinySword = true;
                                    hasRustySword = false;
                                    hasDagger = false;
                                    iron -= 5;
                                    coal -= 10;
                                    diamond -= 1;
                                    Console.WriteLine("du erhitzt dein Rostiges Schwert und schmiedest es in ein Glänzendes Schwert um! Dein Schaden erhöht sich drastisch!");
                                    crafted = true;
                                }
                                else
                                {
                                    Console.WriteLine("Dir fehlen die nötigen Ressourcen!");
                                }
                                break;

                            case "4":
                                if (hasShinySword == true && gold >= 10 && iron >= 20 && diamond >= 3 && ruby >= 1)
                                {
                                    hasShimmeringSword = true;
                                    hasShinySword = false;
                                    gold -= 10;
                                    iron -= 20;
                                    diamond -= 3;
                                    ruby -= 1;
                                    Console.WriteLine("Du Kombinierst sämtliche Erze die du in Höhlen gefunden hast zu einem Starken Schwert. Du fühlst dich so Mächtig wie noch nie!");
                                    crafted = true;
                                }
                                else
                                {
                                    Console.WriteLine("Dir fehlen die nötigen Ressourcen!");
                                }
                                break;

                            case "5":
                                if (hasShimmeringSword == true)
                                {
                                    Console.WriteLine("Momentan weißt du nicht wie du dein Schwert noch besser machen sollst jedoch spürst du den Drang Götter zu Töten wenn du an dieses Schwert Denkst");
                                }
                                else
                                {
                                    Console.WriteLine("Dir fehlen die nötigen Ressourcen!");
                                }
                                break;

                            case "6":
                                Console.WriteLine("Du Verlässt den Shop!");
                                crafted = true;
                                break;
                        }


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


