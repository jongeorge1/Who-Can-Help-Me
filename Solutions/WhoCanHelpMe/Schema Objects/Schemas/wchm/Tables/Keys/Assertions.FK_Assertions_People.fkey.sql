ALTER TABLE [wchm].Assertions
	ADD CONSTRAINT [FK_Assertions_Profiles] 
	FOREIGN KEY (ProfileId)
	REFERENCES [wchm].Profiles ([ProfileId])	

