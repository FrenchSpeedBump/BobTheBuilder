namespace BobTheBuilder
{
    public class QuestInit
    {

        List<Quest> questsCons1 = new();

        List<Quest> questsCons2 = new();

        List<Quest> questsCons3 = new();
    

        public List<Quest> GetQuestsCons1()
        {
            // use this as template for adding quests (shortDesc, longDesc, requirements, phase, price)
            List<string> requirements = new();
            requirements.Add("Concrete");
            questsCons1.Add(new Quest("Build Foundation", "Lay a sturdy foundation using concrete.", requirements , 1, 100, "foundation")); 
            
            return questsCons1;
        }

        public List<Quest> GetQuestsCons2()
        {
            List<string> requirements = new();
            requirements.Add("Bricks");
            

            questsCons2.Add(new Quest("Build Walls", "Raise the walls with solid bricks.", requirements, 2, 80, "walls"));

            return questsCons2;
        }

        public List<Quest> GetQuestsCons3()
        {
            List<string> requirements = new();
            requirements.Add("Wood");
            questsCons3.Add(new Quest("Build Roof", "Add a roof to keep the rain out.", requirements, 3, 60, "roof"));
            return questsCons3;
        }
    }
}
