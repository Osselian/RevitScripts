using System;

namespace BossFight
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        private static void BossFight()
        {
            Random rand = new Random();

            int characterBaseHealth = 80;
            int characterHealth = characterBaseHealth;
            int characterDamage;
            int glacialSpearDamage;
            int characterArmor = 1;
            bool adsorbPain = false;
            int healingTurns = 0;
            int additionalArmorTurns = 0;
            bool isCharacterStunned = false;

            int bossHealth = 50;
            int bossDamage;
            int bossArmor = 2;
            int bossStunTurns = 0;
            int bossAdditionalArmorTurns = 0;

            int turn = 1;
            while (characterHealth > 0 && bossHealth > 0)
            {
                Console.WriteLine("");
                Console.WriteLine($"Turn {turn}!");
                if (isCharacterStunned)
                {
                    Console.WriteLine("You are stunned!");
                    isCharacterStunned = false;
                }
                else
                {
                    Console.WriteLine($"Your health - {characterHealth}. Boss health - {bossHealth}.\n" +
                    $"Your armor - {characterArmor}. Boss armor - {bossArmor}\n" +
                    $"Abilities:\n" +
                    $"A - Attack: Makes 5-7 gamage\n" +
                    $"D - Defend: +2 to Armor for next 3 turns\n" +
                    $"1 - Pierce Armor: Make base Attack and Ignore ememy armor. If enemy is stunned make additional damage (+2)\n" +
                    $"2 - Glacial Spear: Makes 4-6 damage and stun an enemy\n" +
                    $"3 - Adsorb Pain: Next enemy attack will heal you\n" +
                    $"4 - Heal: Restore 2 HP for 4 turns\n");

                    string action = Console.ReadLine();

                    switch (action)
                    {
                        case "1":
                            characterDamage = rand.Next(5, 8);
                            if (bossStunTurns > 0)
                            {
                                bossHealth -= characterDamage + 2;
                                Console.WriteLine($"You make {characterDamage + 2} damage!");
                            }
                            else
                            {
                                bossHealth -= characterDamage;
                                Console.WriteLine($"You make {characterDamage} damage!");
                            }
                            break;
                        case "2":
                            glacialSpearDamage = rand.Next(4, 7);
                            bossHealth -= glacialSpearDamage;
                            Console.WriteLine($"You make {glacialSpearDamage} damage and stun the enemy!");
                            bossStunTurns = 2;
                            break;
                        case "3":
                            adsorbPain = true;
                            break;
                        case "4":
                            characterHealth += 2;
                            if (characterHealth > characterBaseHealth)
                            {
                                characterHealth = characterBaseHealth;
                            }
                            healingTurns = 4;
                            break;
                        case "A":
                            characterDamage = rand.Next(5, 8);
                            bossHealth -= characterDamage - bossArmor;
                            Console.WriteLine($"You make {characterDamage - bossArmor} damage!");
                            break;
                        case "D":
                            if (additionalArmorTurns == 0)
                            {
                                characterArmor += 2;
                                additionalArmorTurns = 3;
                            }
                            else
                            {
                                additionalArmorTurns = 3;
                            }
                            break;
                    }
                }
                Console.ReadKey();

                if (bossStunTurns != 2)
                {
                    int bossAction = rand.Next(1, 6);
                    switch (bossAction)
                    {
                        case 1:
                            bossDamage = rand.Next(5, 8);
                            if (adsorbPain)
                            {
                                characterHealth += bossDamage - characterArmor;
                                Console.WriteLine($"Enemy attack heals you for {bossDamage - characterArmor} HP!");
                                if (characterHealth > characterBaseHealth)
                                {
                                    characterHealth = characterBaseHealth;
                                }
                            }
                            else
                            {
                                characterHealth -= bossDamage - characterArmor;
                                Console.WriteLine($"Enemy damage you for {bossDamage - characterArmor} HP!");
                            }
                            break;
                        case 2:
                            Console.WriteLine($"Enemy make a defence stand!");
                            if (bossAdditionalArmorTurns == 0)
                            {
                                bossArmor += 2;
                                bossAdditionalArmorTurns = 4;
                            }
                            else
                            {
                                bossAdditionalArmorTurns = 4;
                            }
                            break;
                        case 3:
                            Console.WriteLine($"Enemy make a double attack!");
                            for (int i = 0; i < 2; i++)
                            {
                                bossDamage = rand.Next(5, 8);
                                if (adsorbPain)
                                {
                                    characterHealth += bossDamage - characterArmor;
                                    Console.WriteLine($"Enemy attack heals you for {bossDamage - characterArmor} HP!");
                                    if (characterHealth > characterBaseHealth)
                                    {
                                        characterHealth = characterBaseHealth;
                                    }
                                }
                                else
                                {
                                    characterHealth -= bossDamage - characterArmor;
                                    Console.WriteLine($"Enemy damage you for {bossDamage - characterArmor} HP!");
                                }
                            }
                            break;
                        case 4:
                            bossDamage = rand.Next(5, 8);
                            if (adsorbPain)
                            {
                                characterHealth += bossDamage - characterArmor;
                                Console.WriteLine($"Enemy attack heals you for {bossDamage - characterArmor} HP!");
                            }
                            else
                            {
                                characterHealth -= bossDamage - characterArmor;
                                Console.WriteLine($"Enemy damage you for {bossDamage - characterArmor} HP!");
                            }
                            isCharacterStunned = true;
                            break;
                        case 5:
                            bossHealth += 7;
                            Console.WriteLine($"Enemy heals himself for 7 HP!");
                            if (bossHealth > 50)
                            {
                                bossHealth = 50;
                            }
                            break;
                    }
                }
                Console.ReadKey();

                if (healingTurns > 0)
                {
                    characterHealth += 2;
                    if (characterHealth > characterBaseHealth)
                    {
                        characterHealth = characterBaseHealth;
                    }
                    healingTurns--;
                }

                adsorbPain = false;

                if (additionalArmorTurns > 0)
                {
                    additionalArmorTurns--;
                }

                if (bossAdditionalArmorTurns > 0)
                {
                    bossAdditionalArmorTurns--;
                }
                if (bossAdditionalArmorTurns == 0)
                {
                    bossArmor = 2;
                }

                if (bossStunTurns > 0)
                {
                    bossStunTurns--;
                }

                turn++;
                Console.Clear();
            }

            if (characterHealth <= 0 && bossHealth <= 0)
            {
                Console.WriteLine("You both died!");
            }
            else if (bossHealth <= 0)
            {
                Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine("You died!");
            }
        }
    }
}
