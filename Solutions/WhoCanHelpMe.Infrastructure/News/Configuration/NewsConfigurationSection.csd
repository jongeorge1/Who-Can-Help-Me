<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel dslVersion="1.0.0.0" Id="2aa60371-50f6-4fc0-b791-e5210c73839d" namespace="WhoCanHelpMe.Infrastructure.News.Configuration" xmlSchemaNamespace="http://schemas.who-can-help.me/news/configuration" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <configurationElements>
    <configurationElementCollection name="SearchTags" namespace="WhoCanHelpMe.Infrastructure.News.Configuration" xmlItemName="searchTag" codeGenOptions="Indexer, AddMethod, RemoveMethod">
      <itemType>
        <configurationElementMoniker name="/2aa60371-50f6-4fc0-b791-e5210c73839d/SearchTag" />
      </itemType>
    </configurationElementCollection>
    <configurationElement name="SearchTag" namespace="WhoCanHelpMe.Infrastructure.News.Configuration">
      <attributeProperties>
        <attributeProperty name="Name" isRequired="true" isKey="true" isDefaultCollection="false" xmlName="name" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/2aa60371-50f6-4fc0-b791-e5210c73839d/String" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
    <configurationSection name="NewsConfigurationSection" namespace="WhoCanHelpMe.Infrastructure.News.Configuration" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="newsConfigurationSection">
      <attributeProperties>
        <attributeProperty name="NoOfBuzzHeadlines" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="noOfBuzzHeadlines" isReadOnly="false" defaultValue="7">
          <type>
            <externalTypeMoniker name="/2aa60371-50f6-4fc0-b791-e5210c73839d/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="SearchTimeoutSeconds" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="searchTimeoutSeconds" isReadOnly="false" defaultValue="10">
          <type>
            <externalTypeMoniker name="/2aa60371-50f6-4fc0-b791-e5210c73839d/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="NoOfDevTeamHeadlines" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="noOfDevTeamHeadlines" isReadOnly="false" defaultValue="7">
          <type>
            <externalTypeMoniker name="/2aa60371-50f6-4fc0-b791-e5210c73839d/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="BuzzHeadlineTags" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="buzzHeadlineTags" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/2aa60371-50f6-4fc0-b791-e5210c73839d/SearchTags" />
          </type>
        </elementProperty>
        <elementProperty name="DevTeamHeadlineTags" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="devTeamHeadlineTags" isReadOnly="false">
          <type>
            <configurationElementCollectionMoniker name="/2aa60371-50f6-4fc0-b791-e5210c73839d/SearchTags" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
  </configurationElements>
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
</configurationSectionModel>