﻿using ContentService.Resources.Messages;

namespace ContentService.Resources.FormattedMessages;
public static class ContentSuccessMessages
{
	public static string SuccessSample() => string.Format(ContentServiceSuccesses.SuccessSample);
	//	public static string SuccessCreate(string input) => string.Format(Successes.SuccessCreate, input);

}