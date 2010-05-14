ALTER TABLE [wchm].[Assertions]
    ADD CONSTRAINT [UK_Assertions]
    UNIQUE (ProfileId, CategoryId, TagId)