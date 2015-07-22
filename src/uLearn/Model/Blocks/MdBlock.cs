﻿using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Xml.Serialization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using uLearn.Model.Edx.EdxComponents;

namespace uLearn.Model.Blocks
{
	[XmlType("md")]
	public class MdBlock : SlideBlock
	{
		private string markdown;

		[XmlText]
		public string Markdown
		{
			get { return markdown; }
			set { markdown = value.RemoveCommonNesting(); }
		}

		public MdBlock(string markdown)
		{
			if (markdown != null)
				Markdown = markdown.TrimEnd();
		}

		public MdBlock()
		{
		}

		public override string ToString()
		{
			return string.Format("Markdown {0}", Markdown);
		}

		public override Component ToEdxComponents(string displayName, Slide slide, int componentIndex)
		{
			var urlName = slide.Guid + componentIndex;
			var htmlWithUrls = Markdown.GetHtmlWithUrls("/static/" + urlName + "_");
			return new HtmlComponent(urlName, displayName,urlName, htmlWithUrls.Item1, slide.Info.SlideFile.Directory.FullName, htmlWithUrls.Item2);
		}
	}
}