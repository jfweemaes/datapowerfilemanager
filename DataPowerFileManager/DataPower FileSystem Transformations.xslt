<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
  <xsl:template match="/">
    <root>
      <xsl:attribute name="r">
        <xsl:value-of select="/foo/Access/r"/>
      </xsl:attribute>
      <xsl:attribute name="w">
        <xsl:value-of select="/foo/Access/w"/>
      </xsl:attribute>
      <xsl:attribute name="a">
        <xsl:value-of select="/foo/Access/a"/>
      </xsl:attribute>
      <xsl:attribute name="d">
        <xsl:value-of select="/foo/Access/d"/>
      </xsl:attribute>
      <xsl:attribute name="x">
        <xsl:value-of select="/foo/Access/x"/>
      </xsl:attribute>
      <xsl:for-each select="/foo/directory">
        <xsl:variable name="fulldname">
          <xsl:value-of select="@name"/>
        </xsl:variable>
        <xsl:variable name="dname" select="substring-after($fulldname,'/')"/>
        <xsl:element name="{string($dname)}">
          <xsl:attribute name="type">directory</xsl:attribute>
          <xsl:attribute name="r">
            <xsl:value-of select="Access/r"/>
          </xsl:attribute>
          <xsl:attribute name="w">
            <xsl:value-of select="Access/w"/>
          </xsl:attribute>
          <xsl:attribute name="a">
            <xsl:value-of select="Access/a"/>
          </xsl:attribute>
          <xsl:attribute name="d">
            <xsl:value-of select="Access/d"/>
          </xsl:attribute>
          <xsl:attribute name="x">
            <xsl:value-of select="Access/x"/>
          </xsl:attribute>
          <xsl:for-each select="file">
            <xsl:call-template name="file"/>
          </xsl:for-each>
          <xsl:for-each select="directory">
            <xsl:call-template name="directory"/>
          </xsl:for-each>
        </xsl:element>
      </xsl:for-each>
      <xsl:for-each select="/foo/file">
        <xsl:variable name="fname">
          <xsl:value-of select="@name"/>
        </xsl:variable>
        <xsl:element name="{string($fname)}">
          <xsl:attribute name="type">file</xsl:attribute>
          <xsl:attribute name="r">
            <xsl:value-of select="Access/r"/>
          </xsl:attribute>
          <xsl:attribute name="w">
            <xsl:value-of select="Access/w"/>
          </xsl:attribute>
          <xsl:attribute name="a">
            <xsl:value-of select="Access/a"/>
          </xsl:attribute>
          <xsl:attribute name="d">
            <xsl:value-of select="Access/d"/>
          </xsl:attribute>
          <xsl:attribute name="x">
            <xsl:value-of select="Access/x"/>
          </xsl:attribute>
        </xsl:element>
      </xsl:for-each>
    </root>
  </xsl:template>
  <xsl:template name="file">
    <xsl:variable name="fname">
      <xsl:value-of select="@name"/>
    </xsl:variable>
    <xsl:element name="{string($fname)}">
      <xsl:attribute name="type">file</xsl:attribute>
      <xsl:attribute name="r">
        <xsl:value-of select="Access/r"/>
      </xsl:attribute>
      <xsl:attribute name="w">
        <xsl:value-of select="Access/w"/>
      </xsl:attribute>
      <xsl:attribute name="a">
        <xsl:value-of select="Access/a"/>
      </xsl:attribute>
      <xsl:attribute name="d">
        <xsl:value-of select="Access/d"/>
      </xsl:attribute>
      <xsl:attribute name="x">
        <xsl:value-of select="Access/x"/>
      </xsl:attribute>
    </xsl:element>
  </xsl:template>
  <xsl:template name="directory">
    <xsl:variable name="fulldname">
      <xsl:value-of select="@name"/>
    </xsl:variable>
    <xsl:variable name="dname" select="translate(substring-after($fulldname,'/'),'/','-')"/>
    <xsl:element name="{string($dname)}">
      <xsl:attribute name="type">directory</xsl:attribute>
      <xsl:attribute name="r">
        <xsl:value-of select="Access/r"/>
      </xsl:attribute>
      <xsl:attribute name="w">
        <xsl:value-of select="Access/w"/>
      </xsl:attribute>
      <xsl:attribute name="a">
        <xsl:value-of select="Access/a"/>
      </xsl:attribute>
      <xsl:attribute name="d">
        <xsl:value-of select="Access/d"/>
      </xsl:attribute>
      <xsl:attribute name="x">
        <xsl:value-of select="Access/x"/>
      </xsl:attribute>
      <xsl:for-each select="file">
        <xsl:call-template name="file"/>
      </xsl:for-each>
      <xsl:for-each select="directory">
        <xsl:call-template name="directory"/>
      </xsl:for-each>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>
