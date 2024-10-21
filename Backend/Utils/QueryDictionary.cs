namespace PersonalWebsite.Utils
{
    public static class QueryDictionary
    {
        private static readonly Dictionary<string, string> _queries = new()
        {
            {"getCode","\r\n    query submissionDetails($submissionId: Int!) {\r\n  submissionDetails(submissionId: $submissionId) {\r\n    runtime\r\n    runtimeDisplay\r\n    runtimePercentile\r\n    runtimeDistribution\r\n    memory\r\n    memoryDisplay\r\n    memoryPercentile\r\n    memoryDistribution\r\n    code\r\n    timestamp\r\n    statusCode\r\n    user {\r\n      username\r\n      profile {\r\n        realName\r\n        userAvatar\r\n      }\r\n    }\r\n    lang {\r\n      name\r\n      verboseName\r\n    }\r\n    question {\r\n      questionId\r\n      titleSlug\r\n      hasFrontendPreview\r\n    }\r\n    notes\r\n    flagType\r\n    topicTags {\r\n      tagId\r\n      slug\r\n      name\r\n    }\r\n    runtimeError\r\n    compileError\r\n    lastTestcase\r\n    codeOutput\r\n    expectedOutput\r\n    totalCorrect\r\n    totalTestcases\r\n    fullCodeOutput\r\n    testDescriptions\r\n    testBodies\r\n    testInfo\r\n    stdOutput\r\n  }\r\n}\r\n" },
            {"getSubmissionId",@" query submissionList($offset: Int!, $limit: Int!, $lastKey: String, $questionSlug: String!, $lang: Int, $status: Int) {
        questionSubmissionList(
            offset: $offset
            limit: $limit
            lastKey: $lastKey
            questionSlug: $questionSlug
            lang: $lang
            status: $status
        ) {
            lastKey
            hasNext
            submissions {
                id
                title
                titleSlug
                status
                statusDisplay
                lang
                langName
                runtime
                timestamp
                url
                isPending
                memory
                hasNotes
                notes
                flagType
                topicTags {
                    id
                }
            }
        }}" },
            {"getTags","\r\n    query singleQuestionTopicTags($titleSlug: String!) {\r\n  question(titleSlug: $titleSlug) {\r\n    topicTags {\r\n      name\r\n      slug\r\n    }\r\n  }\r\n}\r\n    " },
            {"saveProblem","\r\n    query questionTitle($titleSlug: String!) {\r\n  question(titleSlug: $titleSlug) {\r\n    questionId\r\n    questionFrontendId\r\n    title\r\n    titleSlug\r\n    isPaidOnly\r\n    difficulty\r\n    likes\r\n    dislikes\r\n    categoryTitle\r\n  }\r\n}\r\n    " }
        };
        public static string? GetQuery(string name)
        {
            return _queries[name];
        }
            
    }
}
