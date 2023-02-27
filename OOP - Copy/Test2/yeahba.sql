-- Table: public.user_record

-- DROP TABLE IF EXISTS public.user_record;

CREATE TABLE IF NOT EXISTS public.user_record
( CHECK (logdate >=DATE '2023-15-02' AND logdate < DATE '2023-16-02')
    -- Inherited from table public.tbl_record: id_no integer NOT NULL,
    -- Inherited from table public.tbl_record: fname text COLLATE pg_catalog."default",
    -- Inherited from table public.tbl_record: dpart text COLLATE pg_catalog."default",
    -- Inherited from table public.tbl_record: timein_am text COLLATE pg_catalog."default",
    -- Inherited from table public.tbl_record: timeout_am text COLLATE pg_catalog."default"
)
    INHERITS (public.tbl_record)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.user_record
    OWNER to postgres;