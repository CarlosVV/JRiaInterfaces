CREATE TABLE [dbo].[systblApp_TaxReceipt_Store] (
    [fStoreId]      INT           NOT NULL,
    [fName]         NVARCHAR (80) NULL,
    [fLocation]     NVARCHAR (80) NULL,
    [fSiiCode]      NVARCHAR (20) NULL,
    [fPhoneNumber1] NVARCHAR (50) NULL,
    [fPhoneNumber2] NVARCHAR (50) NULL,
    [fDisabled]     BIT           CONSTRAINT [DF_systblApp_TaxReceipt_Store_fDisabled] DEFAULT ((0)) NULL,
    [fDelete]       BIT           CONSTRAINT [DF_systblApp_TaxReceipt_Store_fDelete] DEFAULT ((0)) NULL,
    [fChanged]      BIT           CONSTRAINT [DF_systblApp_TaxReceipt_Store_fChanged] DEFAULT ((0)) NULL,
    [fTime]         DATETIME      CONSTRAINT [DF_systblApp_TaxReceipt_Store_fTime] DEFAULT (getdate()) NULL,
    [fModified]     DATETIME      NULL,
    [fModifiedID]   INT           NULL,
    CONSTRAINT [systblApp_TaxReceipt_Store] PRIMARY KEY CLUSTERED ([fStoreId] ASC)
);

