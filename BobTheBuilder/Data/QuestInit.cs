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
            questsCons1.Add(new Quest("Dig Foundation", "You will have to pay the construction office to dig.", null , 1, 100)); 
            
            return questsCons1;
        }

        public List<Quest> GetQuestsCons2()
        {
            List<Material> requirements = new() { new Material("Wood", "A sturdy piece of wood.", 0.8, 20)};
            

            questsCons2.Add(new Quest("Dig Foundation", "You will have to pay the construction office to dig.", requirements, 1, 100));

            return questsCons2;
        }

        public List<Quest> GetQuestsCons3()
        {
            return questsCons3;
        }
    }
}
