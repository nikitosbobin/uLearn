﻿using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace uLearn.Model.Edx
{
	[XmlRoot("course")]
	public class CourseWithChapters : EdxItem
	{
		[XmlIgnore]
		public override string SubfolderName { get { return "course"; } }

		[XmlAttribute("display_name")]
		public string DisplayName;

		[XmlAttribute("advanced_modules")]
		public string AdvancedModules;

		[XmlAttribute("lti_passports")]
		public string LtiPassports;

		[XmlAttribute("use_latex_compiler")]
		public bool UseLatexCompiler;

		[XmlElement("chapter")]
		public ChapterReference[] ChapterReferences { get; set; }

		[XmlIgnore]
		public Chapter[] Chapters;

		public CourseWithChapters()
		{
		}

		public CourseWithChapters(string urlName, string displayName, string[] advancedModules, string[] ltiPassports, bool useLatexCompiler, Chapter[] chapters)
		{
			UrlName = urlName;
			DisplayName = displayName;
			AdvancedModules = JsonConvert.SerializeObject(advancedModules);
			LtiPassports = JsonConvert.SerializeObject(ltiPassports);
			UseLatexCompiler = useLatexCompiler;
			Chapters = chapters;
			ChapterReferences = chapters.Select(x => x.GetReference()).ToArray();
		}

		public override void SaveAdditional(string folderName)
		{
			foreach (var chapter in Chapters)
				chapter.Save(folderName);
		}
	}
}