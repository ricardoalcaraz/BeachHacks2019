PGDMP                         w        
   PolitiFact    11.2    11.2                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                       false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                       false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                       false                       1262    16407 
   PolitiFact    DATABASE     �   CREATE DATABASE "PolitiFact" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'English_United States.1252' LC_CTYPE = 'English_United States.1252';
    DROP DATABASE "PolitiFact";
             cinna    false            �            1259    16418    politicalparty    TABLE     v   CREATE TABLE public.politicalparty (
    political_party_id integer NOT NULL,
    party_name character varying(50)
);
 "   DROP TABLE public.politicalparty;
       public         postgres    false            �            1259    16416 %   politicalparty_political_party_id_seq    SEQUENCE     �   CREATE SEQUENCE public.politicalparty_political_party_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 <   DROP SEQUENCE public.politicalparty_political_party_id_seq;
       public       postgres    false    197                       0    0 %   politicalparty_political_party_id_seq    SEQUENCE OWNED BY     o   ALTER SEQUENCE public.politicalparty_political_party_id_seq OWNED BY public.politicalparty.political_party_id;
            public       postgres    false    196            �            1259    16426    presidentialcandidate    TABLE     �   CREATE TABLE public.presidentialcandidate (
    user_id integer NOT NULL,
    name character varying(50) NOT NULL,
    age integer NOT NULL,
    state character varying(2) NOT NULL,
    political_party_id integer NOT NULL
);
 )   DROP TABLE public.presidentialcandidate;
       public         postgres    false            �            1259    16424 !   presidentialcandidate_user_id_seq    SEQUENCE     �   CREATE SEQUENCE public.presidentialcandidate_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 8   DROP SEQUENCE public.presidentialcandidate_user_id_seq;
       public       postgres    false    199                       0    0 !   presidentialcandidate_user_id_seq    SEQUENCE OWNED BY     g   ALTER SEQUENCE public.presidentialcandidate_user_id_seq OWNED BY public.presidentialcandidate.user_id;
            public       postgres    false    198            �            1259    16455    tweet    TABLE     �   CREATE TABLE public.tweet (
    tweet_id integer NOT NULL,
    text character varying(500) NOT NULL,
    twitter_user_id bigint NOT NULL,
    twitter_name character varying(50) NOT NULL,
    political_candidate integer NOT NULL
);
    DROP TABLE public.tweet;
       public         postgres    false            �            1259    16453    tweet_tweet_id_seq    SEQUENCE     �   CREATE SEQUENCE public.tweet_tweet_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.tweet_tweet_id_seq;
       public       postgres    false    201                       0    0    tweet_tweet_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.tweet_tweet_id_seq OWNED BY public.tweet.tweet_id;
            public       postgres    false    200            �
           2604    16421 !   politicalparty political_party_id    DEFAULT     �   ALTER TABLE ONLY public.politicalparty ALTER COLUMN political_party_id SET DEFAULT nextval('public.politicalparty_political_party_id_seq'::regclass);
 P   ALTER TABLE public.politicalparty ALTER COLUMN political_party_id DROP DEFAULT;
       public       postgres    false    197    196    197            �
           2604    16429    presidentialcandidate user_id    DEFAULT     �   ALTER TABLE ONLY public.presidentialcandidate ALTER COLUMN user_id SET DEFAULT nextval('public.presidentialcandidate_user_id_seq'::regclass);
 L   ALTER TABLE public.presidentialcandidate ALTER COLUMN user_id DROP DEFAULT;
       public       postgres    false    199    198    199            �
           2604    16458    tweet tweet_id    DEFAULT     p   ALTER TABLE ONLY public.tweet ALTER COLUMN tweet_id SET DEFAULT nextval('public.tweet_tweet_id_seq'::regclass);
 =   ALTER TABLE public.tweet ALTER COLUMN tweet_id DROP DEFAULT;
       public       postgres    false    201    200    201                      0    16418    politicalparty 
   TABLE DATA               H   COPY public.politicalparty (political_party_id, party_name) FROM stdin;
    public       postgres    false    197   �!                 0    16426    presidentialcandidate 
   TABLE DATA               ^   COPY public.presidentialcandidate (user_id, name, age, state, political_party_id) FROM stdin;
    public       postgres    false    199   �!                 0    16455    tweet 
   TABLE DATA               c   COPY public.tweet (tweet_id, text, twitter_user_id, twitter_name, political_candidate) FROM stdin;
    public       postgres    false    201   �"                  0    0 %   politicalparty_political_party_id_seq    SEQUENCE SET     S   SELECT pg_catalog.setval('public.politicalparty_political_party_id_seq', 2, true);
            public       postgres    false    196                        0    0 !   presidentialcandidate_user_id_seq    SEQUENCE SET     O   SELECT pg_catalog.setval('public.presidentialcandidate_user_id_seq', 8, true);
            public       postgres    false    198            !           0    0    tweet_tweet_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.tweet_tweet_id_seq', 1, false);
            public       postgres    false    200            �
           2606    16423 "   politicalparty politicalparty_pkey 
   CONSTRAINT     p   ALTER TABLE ONLY public.politicalparty
    ADD CONSTRAINT politicalparty_pkey PRIMARY KEY (political_party_id);
 L   ALTER TABLE ONLY public.politicalparty DROP CONSTRAINT politicalparty_pkey;
       public         postgres    false    197            �
           2606    16433 4   presidentialcandidate presidentialcandidate_name_key 
   CONSTRAINT     o   ALTER TABLE ONLY public.presidentialcandidate
    ADD CONSTRAINT presidentialcandidate_name_key UNIQUE (name);
 ^   ALTER TABLE ONLY public.presidentialcandidate DROP CONSTRAINT presidentialcandidate_name_key;
       public         postgres    false    199            �
           2606    16431 0   presidentialcandidate presidentialcandidate_pkey 
   CONSTRAINT     s   ALTER TABLE ONLY public.presidentialcandidate
    ADD CONSTRAINT presidentialcandidate_pkey PRIMARY KEY (user_id);
 Z   ALTER TABLE ONLY public.presidentialcandidate DROP CONSTRAINT presidentialcandidate_pkey;
       public         postgres    false    199            �
           2606    16463    tweet tweet_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.tweet
    ADD CONSTRAINT tweet_pkey PRIMARY KEY (tweet_id);
 :   ALTER TABLE ONLY public.tweet DROP CONSTRAINT tweet_pkey;
       public         postgres    false    201            �
           2606    16434 C   presidentialcandidate presidentialcandidate_political_party_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.presidentialcandidate
    ADD CONSTRAINT presidentialcandidate_political_party_id_fkey FOREIGN KEY (political_party_id) REFERENCES public.politicalparty(political_party_id);
 m   ALTER TABLE ONLY public.presidentialcandidate DROP CONSTRAINT presidentialcandidate_political_party_id_fkey;
       public       postgres    false    199    2702    197            �
           2606    16464 $   tweet tweet_political_candidate_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.tweet
    ADD CONSTRAINT tweet_political_candidate_fkey FOREIGN KEY (political_candidate) REFERENCES public.presidentialcandidate(user_id);
 N   ALTER TABLE ONLY public.tweet DROP CONSTRAINT tweet_political_candidate_fkey;
       public       postgres    false    201    2706    199               %   x�3�tI��O.J,�2�J-(M��LN������ ��f         �   x���j�@������;w��1�K�QT�����-���Ɍ\�O���;��x���~��U%��a7Gi*,��h��Ev��&�[�90�s�o�X,��b!�D��1=�"�5�������N������[�:��裿�U؟P�և๧UA��n<\���ة<����}i�c�?��4�            x������ � �     