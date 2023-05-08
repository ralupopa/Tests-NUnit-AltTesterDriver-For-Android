namespace TrashCat.Tests.pages
{
    public class GamePlay : BasePage
    {
        public GamePlay(AltDriver driver) : base(driver)
        {
        }

        public AltObject PauseButton { get => Driver.WaitForObject(By.NAME, "pauseButton", timeout: 2); }
        public AltObject Character { get => Driver.WaitForObject(By.NAME, "PlayerPivot"); }

        public bool IsDisplayed()
        {
            if (PauseButton != null && Character != null)
            {
                return true;
            }
            return false;
        }
        public void TapPause()
        {
            PauseButton.Tap();
        }
        public int GetCurrentLife()
        {
            return Character.GetComponentProperty<int>("CharacterInputController", "currentLife", "Assembly-CSharp");
        }
        public void Jump(AltObject character)
        {
            character.CallComponentMethod<string>("CharacterInputController", "Jump", "Assembly-CSharp", new object[]{});
        }
        public void Slide(AltObject character)
        {
            character.CallComponentMethod<string>("CharacterInputController", "Slide", "Assembly-CSharp", new object[]{});
        }        
        public void MoveRight(AltObject character)
        { 
            character.CallComponentMethod<string>("CharacterInputController", "ChangeLane", "Assembly-CSharp", new string[]{"1"});
        }
        public void MoveLeft(AltObject character)
        { 
            character.CallComponentMethod<string>("CharacterInputController", "ChangeLane", "Assembly-CSharp", new string[]{"-1"});
        }
        public void SetCheatInvincible(string flag)
        {
            Character.CallComponentMethod<string>("CharacterInputController", "CheatInvincible",  "Assembly-CSharp", new string[]{flag});
        }
        public bool GetCheatInvincible()
        {
            return Character.CallComponentMethod<bool>("CharacterInputController", "IsCheatInvincible", "Assembly-CSharp", new object[] { });
        }

        public void SetCurrentLife(int valueToSet)
        {
            Assert.NotNull(Character);
            Assert.That(GetCurrentLife(), Is.EqualTo(3));

            Character.SetComponentProperty("CharacterInputController", "currentLife", valueToSet, "Assembly-CSharp");
        }
        public void AvoidObstacles(int numberOfObstacles)
        {
            var character = Character;
            bool movedLeft = false;
            bool movedRight = false;
            
            SetCheatInvincible("true");

            for (int i = 0; i < numberOfObstacles; i++)
            {
                var allObstacles = Driver.FindObjectsWhichContain(By.NAME, "Obstacle");
                allObstacles.Sort((x, y) => x.worldZ.CompareTo(y.worldZ));
                allObstacles.RemoveAll(obs => obs.worldZ < character.worldZ);
                var obstacle = allObstacles[0];

                System.Console.WriteLine("Obstacle: " + obstacle.name + ", z:" + obstacle.worldZ + ", x:" + obstacle.worldX);
                System.Console.WriteLine("Next: " + allObstacles[1].name + ", z:" + allObstacles[1].worldZ + ", x:" + allObstacles[1].worldX);

                while (obstacle.worldZ - character.worldZ > 5)
                {
                    obstacle = Driver.FindObject(By.ID, obstacle.id.ToString());
                    character = Driver.FindObject(By.NAME, "PlayerPivot");
                }
                if (obstacle.name.Contains("ObstacleHighBarrier"))
                {
                    Slide(character);
                }
                else
                if (obstacle.name.Contains("ObstacleLowBarrier") || obstacle.name.Contains("Rat"))
                {
                    Jump(character);
                }
                else
                {
                    if (obstacle.worldZ == allObstacles[1].worldZ)
                    {
                        if (obstacle.worldX == character.worldX)
                        {
                            if (allObstacles[1].worldX == -1.5f)
                            {
                                MoveRight(character);
                                movedRight = true;
                            }
                            else
                            {
                                MoveLeft(character);
                                movedLeft = true;
                            }
                        }
                        else
                        {
                            if (allObstacles[1].worldX == character.worldX)
                            {
                                if (obstacle.worldX == -1.5f)
                                {
                                    MoveRight(character);
                                    movedRight = true;
                                }
                                else
                                {
                                    MoveLeft(character);
                                    movedLeft = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (obstacle.worldX == character.worldX)
                        {
                            MoveRight(character);
                            movedRight = true;
                        }
                    }
                }
                while (character.worldZ - 3 < obstacle.worldZ && character.worldX < 99)
                {
                    obstacle = Driver.FindObject(By.ID, obstacle.id.ToString());
                    character = Driver.FindObject(By.NAME, "PlayerPivot");
                }
                if (movedRight)
                {
                    MoveLeft(character);
                    movedRight = false;
                }
                if (movedLeft)
                {
                    MoveRight(character);
                    movedRight = false;
                }
            }
            SetCheatInvincible("false");
        }
    }
}