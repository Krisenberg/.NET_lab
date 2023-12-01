using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List_07
{
    internal class Tasks_1_2
    {
        public static void GroupSortedStudentsBySurnameNGroups(List<StudentWithTopics> students, int n)
        {
            int rowIndex = 0;
            var studGroups = from stud_id in (
                                from stud in students
                                orderby stud.Name, stud.Index
                                select new { student = stud, groupID = (rowIndex++) / n }
                             )
                             group stud_id by stud_id.groupID into studGroup
                             select new
                             {
                                 GroupID = studGroup.Key,
                                 Students = studGroup.Select(s => s.student)
                             };

            Console.WriteLine("Sorted students by Name (and Index in case of equal names) grouped");
            Console.WriteLine($"in {n}-element groups:\n");
            foreach (var group in studGroups)
            {
                Console.WriteLine("Group number: " + (group.GroupID + 1));
                group.Students.ToList().ForEach(stud => Console.WriteLine("  " + stud));
            }
        }

        public static void GroupTopicsByIncidence(List<StudentWithTopics> students)
        {
            var topicsGroup = from stud in students
                              from topic in stud.Topics
                              group topic by topic into topicGroup
                              orderby topicGroup.Count() descending
                              select new
                              {
                                  Topic = topicGroup.Key,
                                  Count = topicGroup.Count()
                              };

            Console.WriteLine("Sorted topics by their incidence:");
            foreach (var group in topicsGroup)
            {
                Console.WriteLine($"Count: {group.Count} ---> Topic: {group.Topic}");
            }
        }

        public static void GroupTopicsByIncidenceWithinGender(List<StudentWithTopics> students)
        {
            var topicsGroup = from genderTopicCountGroup in (
                                from genderTopicItem in (
                                    from stud in students
                                    from topic in stud.Topics
                                    select new
                                    {
                                        stud.Gender,
                                        Topic = topic
                                    }
                                  )
                                group genderTopicItem by genderTopicItem into genderTopicGroup
                                orderby genderTopicGroup.Count() descending
                                select new
                                {
                                    genderTopicGroup.Key.Topic,
                                    genderTopicGroup.Key.Gender,
                                    Count = genderTopicGroup.Count()
                                }
                              )
                              group genderTopicCountGroup by genderTopicCountGroup.Gender into genderGroup
                              select genderGroup;
            //var topicsGroup = from genderTopic in (
            //                        from stud in students
            //                        from topic in stud.Topics
            //                        select new
            //                        {
            //                            Gender = stud.Gender,
            //                            Topic = topic
            //                        }
            //                      )
            //                  group genderTopic by genderTopic into topicGroup
            //                  orderby topicGroup.Count() descending
            //                  select new
            //                  {
            //                      Topic = topicGroup.Key.Topic,
            //                      Gender = topicGroup.Key.Gender,
            //                      Count = topicGroup.Count()
            //                  };

            Console.WriteLine("\n\nSorted topics by their incidence withing gender:");
            foreach (var group in topicsGroup)
            {
                //Console.WriteLine($"Topic: {group.Topic}, Gender: {group.Gender}, Count: {group.Count}");
                Console.WriteLine($"Gender: {group.Key}");
                group.ToList().ForEach(groupItem => Console.WriteLine($"\tCount: {groupItem.Count} ---> Topic: {groupItem.Topic}"));
            }
        }



        public static void ConvertStudentsWithTopicsToStudents(List<StudentWithTopics> studentsWithTopics)
        {
            //var topicsList = new List<Topic>() {
            //    new Topic(1, "C#"),
            //    new Topic(2, "PHP"),
            //    new Topic(3, "algorithms"),
            //    new Topic(4, "C++"),
            //    new Topic(5, "fuzzy logic"),
            //    new Topic(6, "Basic"),
            //    new Topic(7, "Java"),
            //    new Topic(8, "JavaScript"),
            //    new Topic(9, "neural networks"),
            //    new Topic(10, "web programming")
            //};

            int idCounter = 1;
            var topicsList = (from distinctTopic in (
                                (from stud in studentsWithTopics
                                 from topic in stud.Topics
                                 select topic).Distinct())
                              select new Topic((idCounter++), distinctTopic)).ToList();

            var getTopicsIdBasedOnName = new Func<string, List<Topic>, int>
                                         ((topicName, topics) => {
                                             foreach (var topic in topics)
                                             {
                                                 if (topic.Name == topicName)
                                                     return topic.Id;
                                             }
                                             return -1;
                                         });

            var students = from studWithTopics in studentsWithTopics
                           select new Student(
                               studWithTopics.Id,
                               studWithTopics.Index,
                               studWithTopics.Name,
                               studWithTopics.Gender,
                               studWithTopics.Active,
                               studWithTopics.DepartmentId,
                               (from topic in studWithTopics.Topics
                                select getTopicsIdBasedOnName(topic, topicsList)
                               ).ToList());

            Console.WriteLine("Students before conversion[StudentsWithTopics]");
            foreach (var studWithTopics in studentsWithTopics)
            {
                Console.WriteLine(studWithTopics);
            }

            Console.WriteLine("\nTopics with assigned ID");
            foreach (var topic in topicsList)
            {
                Console.WriteLine(topic);
            }

            Console.WriteLine("\nStudents after conversion[Students]");
            foreach (var stud in students)
            {
                Console.WriteLine(stud);
            }
        }
    }
}
