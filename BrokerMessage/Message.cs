namespace BrokerMessage
{
    public static class Message
    {
        private static Queue<string> Save = new Queue<string>();
        public static void PushMessage(string value)
        {
            Save.Enqueue(value);
        }

  
        public static string GetMessage ()
        {
            var msg = string.Empty;
            try
            {
                msg = Save.Dequeue();
            }
            catch
            {
               Console.WriteLine("Очередь пуста");
            }
            return msg;
        }
    }
}

//Save.Contains();

//Queue<string> success = Save.Contains(Save);


//var success1 = people.TryDequeue(out var person1);  // success1 = true
//if (success1) Console.WriteLine(person1); // Tom