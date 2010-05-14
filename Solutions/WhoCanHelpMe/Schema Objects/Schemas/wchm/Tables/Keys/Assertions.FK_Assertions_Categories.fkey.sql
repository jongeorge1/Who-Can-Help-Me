ALTER TABLE [wchm].Assertions
	ADD CONSTRAINT [FK_Assertions_Categories] 
	FOREIGN KEY (CategoryId)
	REFERENCES [wchm].Categories ([CategoryId])	

