namespace StudentsMarks
{
    using System;
    using System.Collections.Generic;

    public static class LinqQuery
    {
        public static IEnumerable<Test> Execute(List<Test> allTests, string queryRequest)
        {
            string[] requestSplit = queryRequest.Split(' ');

            string sortRequestProperty = string.Empty;
            string sortRequestOrder = string.Empty;

            IEnumerable<Test> query = allTests.Select(t => t);
            var filterParameters = new List<Func<Test, bool>>();

            for (int i = 0; i < requestSplit.Length; i++)
            {
                if (requestSplit[i] == "-name" && i + 1 < requestSplit.Length)
                {
                    string name = requestSplit[i + 1];
                    filterParameters.Add(test => test.Name.Contains(name));
                    i++;
                    continue;
                }

                if (requestSplit[i] == "-minmark" && i + 1 < requestSplit.Length)
                {
                    int minMark = ConvertStringToInt(requestSplit[i + 1]);
                    filterParameters.Add(test => test.Mark >= minMark);
                    i++;
                    continue;
                }

                if (requestSplit[i] == "-maxmark" && i + 1 < requestSplit.Length)
                {
                    int maxMark = ConvertStringToInt(requestSplit[i + 1]);
                    filterParameters.Add(test => test.Mark <= maxMark);
                    i++;
                    continue;
                }

                if (requestSplit[i] == "-datefrom" && i + 1 < requestSplit.Length)
                {
                    DateTime dateFrom = ConvertStringToDateTime(requestSplit[i + 1]);
                    filterParameters.Add(test => test.Date >= dateFrom);
                    i++;
                    continue;
                }

                if (requestSplit[i] == "-dateto" && i + 1 < requestSplit.Length)
                {
                    DateTime dateTo = ConvertStringToDateTime(requestSplit[i + 1]);
                    filterParameters.Add(test => test.Date <= dateTo);
                    i++;
                    continue;
                }

                if (requestSplit[i] == "-test" && i + 1 < requestSplit.Length)
                {
                    string subject = requestSplit[i + 1];
                    filterParameters.Add(test => test.Subject.Contains(subject));
                    i++;
                    continue;
                }

                if (requestSplit[i] == "-sort")
                {
                    if (i + 2 < requestSplit.Length)
                    {
                        sortRequestProperty = requestSplit[i + 1];
                        sortRequestOrder = requestSplit[i + 2];
                        i += 2;
                        continue;
                    }
                    else
                    {
                        throw new ArgumentException("Sorting request is incorrect (example - sort name asc)");
                    }
                }
            }

            query = ImplementFilterParam(query, filterParameters);

            if (sortRequestProperty != string.Empty && sortRequestOrder != string.Empty)
            {
                query = ImplementSortingParameters(sortRequestProperty, sortRequestOrder, query);
            }

            return query;
        }

        private static IEnumerable<Test> ImplementSortingParameters(string sortRequestProperty, string sortRequestOrder, IEnumerable<Test> query)
        {
            Func<Test, object> propertySortBy = sortRequestProperty.ToLower() switch
            {
                "name" => test => test.Name,
                "test" => test => test.Subject,
                "mark" => test => test.Mark,
                "date" => test => test.Date,
                _ => throw new ArgumentException("Sort parameter is incorrect"),
            };
            query = sortRequestOrder.ToLower() switch
            {
                "asc" => query.OrderBy(propertySortBy),
                "desc" => query.OrderByDescending(propertySortBy),
                _ => throw new ArgumentException("Sort order is incorrect"),
            };

            return query;
        }

        private static int ConvertStringToInt(string stringToConvert)
        {
            return int.TryParse(stringToConvert, out int result)
                ? result
                : throw new ArgumentException("Mark request is incorrect");
        }

        private static DateTime ConvertStringToDateTime(string stringtoConvert)
        {
            bool isDateTime = DateTime.TryParseExact(stringtoConvert, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime result);
            if (isDateTime)
            {
                return result;
            }
            else
            {
                throw new ArgumentException("Date request is incorrect(Exact date formating d/M/yyy)");
            }
        }

        private static IEnumerable<Test> ImplementFilterParam(IEnumerable<Test> queryToFilter, List<Func<Test, bool>> filterParameters)
        {
            foreach (Func<Test, bool> param in filterParameters)
            {
                queryToFilter = queryToFilter.Where(param);
            }

            return queryToFilter;
        }
    }
}