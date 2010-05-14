ALTER TABLE [wchm].Assertions
	ADD CONSTRAINT [FK_Assertions_Tags] 
	FOREIGN KEY (TagId)
	REFERENCES [wchm].Tags ([TagId])	

