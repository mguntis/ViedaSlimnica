<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="html" indent="yes"/>

  <xsl:template match="/">
    <html>
      <head>
        <title>Paroles noteikumi</title>
        <style type="text/css">
          .odd {background-color:#ccc}
          .table { font-family:'Segoe UI',Verdana,Arial; font-size:10pt; border-color:#ccc; }
        </style>
      </head>
      <body>
        <h2>Paroles noteikumi</h2>
        <table border="1" cellpadding="1" cellspacing="0" class="table">       
            <tr class="odd">
              <td>Paroles ilgums</td>
              <td align="center">
                <xsl:value-of select="PasswordPolicy/Password/duration"/> dienas.
              </td>
            </tr>
            <tr>
              <td>Paroles minimālais garums</td>
              <td align="center">
                <xsl:value-of select="PasswordPolicy/Password/minLength"/>
              </td>
            </tr>
            <tr class="odd">
              <td>Paroles maksimālais garums</td>
              <td align="center">
                <xsl:value-of select="PasswordPolicy/Password/maxLength"/>
              </td>
            </tr>
          <tr>
            <td>Minimālais ciparu skaits</td>
            <td align="center">
              <xsl:value-of select="PasswordPolicy/Password/numsLength"/>
            </td>
          </tr>
          <tr class="odd">
            <td>Minimālais lielo burtu skaits</td>
            <td align="center">
              <xsl:value-of select="PasswordPolicy/Password/upperLength"/>
            </td>
          </tr>
          <tr>
            <td>Minimālais simbolu skaits</td>
            <td align="center">
              <xsl:value-of select="PasswordPolicy/Password/specialLength"/>
            </td>
          </tr>
          <tr class="odd">
            <td>Atļautie īpašie simboli</td>
            <td align="center">
              <xsl:value-of select="PasswordPolicy/Password/specialChars"/>
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
