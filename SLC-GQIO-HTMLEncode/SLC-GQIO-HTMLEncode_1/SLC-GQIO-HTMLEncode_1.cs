using Skyline.DataMiner.Analytics.GenericInterface;
using System.Web;

[GQIMetaData(Name = "HTML Encode")]
public class HTMLEncodeOperator : IGQIRowOperator, IGQIInputArguments
{
	private GQIColumnDropdownArgument _htmlColumnArg = new GQIColumnDropdownArgument("Column") { IsRequired = true, Types = new GQIColumnType[] { GQIColumnType.String } };

	private GQIColumn _htmlColumn;

	public GQIArgument[] GetInputArguments()
	{
		return new GQIArgument[] { _htmlColumnArg };
	}

	public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
	{
		_htmlColumn = args.GetArgumentValue(_htmlColumnArg);
		return new OnArgumentsProcessedOutputArgs();
	}

	public void HandleRow(GQIEditableRow row)
	{
		string html;
		if (!row.TryGetValue(_htmlColumn, out html))
			return;
		var encoded = HttpUtility.HtmlEncode(html);
		row.SetValue(_htmlColumn, encoded, encoded);
	}
}