using NHibernate.Linq.Expressions;

namespace NHibernate.Linq.Tests
{
	public static class SqlFunctionExtensions
	{
		/// <summary>
		/// CREATE FUNCTION [dbo].[fnEncrypt] ( @Pwd varchar(max) ) RETURNS varbinary(max) AS
		/// BEGIN
		///		DECLARE @PwdEnc varbinary(max)
		///		SELECT @PwdEnc = convert(varbinary(max), pwdencrypt(@Pwd))
		///		RETURN @PwdEnc
		/// END
		/// GO
		/// </summary>
		/// <param name="methods"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		[SqlFunction("dbo")]
		public static byte[] fnEncrypt(this IDbMethods methods, string value)
		{
			return null;
		}
	}
}