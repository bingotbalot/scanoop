-- Column: public.tbl_user.id_no

-- ALTER TABLE IF EXISTS public.tbl_user DROP COLUMN IF EXISTS id_no;

ALTER TABLE IF EXISTS public.tbl_user
    ADD COLUMN id_no integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 );
insert into tbl_record