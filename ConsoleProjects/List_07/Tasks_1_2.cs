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
            var topicsGroup = from topicGroup in (
                                from genderTopic in (
                                    from stud in students
                                    from topic in stud.Topics
                                    select new
                                    {
                                        Gender = stud.Gender,
                                        Topic = topic
                                    }
                                  )
                                  group genderTopic by genderTopic into topicGroup
                                  orderby topicGroup.Count() descending
                                  select new
                                  {
                                      Topic = topicGroup.Key.Topic,
                                      Gender = topicGroup.Key.Gender,
                                      Count = topicGroup.Count()
                                  }
                                )
                                group topicGroup by topicGroup.Gender into genderGroup
                                select genderGroup;

            Console.WriteLine("\n\nSorted topics by their incidence withing gender:");
            foreach (var group in topicsGroup)
            {
                Console.WriteLine($"Gender: {group.Key}");
                group.ToList().ForEach(groupItem => Console.WriteLine($"\tCount: {groupItem.Count} ---> Topic: {groupItem.Topic}"));
            }
        }
    }
}
