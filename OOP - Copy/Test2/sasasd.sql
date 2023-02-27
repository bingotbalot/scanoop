-- Table: public.user_record

-- DROP TABLE IF EXISTS public.user_record;

CREATE TABLE IF NOT EXISTS public.user_record
(
    -- Inherited from table public.tbl_record: id_no integer NOT NULL,
    -- Inherited from table public.tbl_record: fname text COLLATE pg_catalog."default",
    -- Inherited from table public.tbl_record: dpart text COLLATE pg_catalog."default",
    -- Inherited from table public.tbl_record: timein_am text COLLATE pg_catalog."default",
    -- Inherited from table public.tbl_record: timeout_am text COLLATE pg_catalog."default",
    user_id integer,
    id integer NOT NULL DEFAULT nextval('user_record_id_seq'::regclass),
    CONSTRAINT user_record_pkey PRIMARY KEY (id),
    CONSTRAINT user_record_user_id_fkey FOREIGN KEY (user_id)
        REFERENCES public.tbl_record (id_no) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
    INHERITS (public.tbl_record)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.user_record
    OWNER to postgres;